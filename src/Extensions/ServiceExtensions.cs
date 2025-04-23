using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Wapi.src.Http;
using Wapi.src.Utilities;

namespace Wapi.src.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddWapi(this IServiceCollection services, Action<WhatsappConfig> config)
        {
            services.Configure(config);

            services.AddHttpClient<WhatsappClient>((provider, client) =>
            {
                var options = provider.GetRequiredService<IOptions<WhatsappConfig>>().Value;

                var path = $"{options.BaseUrl}/{options.ApiVersion}/{options.PhoneNumberId}/messages";

                client.BaseAddress = new Uri(path);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {options.AccessToken}");
                client.Timeout = TimeSpan.FromSeconds(180);
            });

            services.AddSingleton<IWapi, Wapi>();

            // inject EncryptionHelper
            services.AddScoped<EncryptionHelper>();

            return services;
        }
    }
}
