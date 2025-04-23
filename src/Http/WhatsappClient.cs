using ErrorOr;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using Wapi.src.MessageResponse;
using Wapi.src.OutgoingMessageModels;

namespace Wapi.src.Http
{
    public class WhatsappClient(ILogger<WhatsappClient> logger, HttpClient httpClient)
    {
        public async Task<ErrorOr<OutBoundMessageResponse>> SendAsync(SendReadReceipt message)
        {
            try
            {
                var jsonObj = JsonConvert.SerializeObject(message);

                var payload = new StringContent(jsonObj, Encoding.UTF8, "application/json");
                var temp = await payload.ReadAsStringAsync();
                var response = await httpClient.PostAsync("messages", payload);

                var stringResponse = await response.Content.ReadAsStringAsync();
                logger.LogInformation("Response from whatsapp Post::{stringResponse}", stringResponse);

                response.EnsureSuccessStatusCode();

                return JsonConvert.DeserializeObject<OutBoundMessageResponse>(stringResponse)!;
            }
            catch (Exception ex)
            {
                return new Error[] { Error.Failure(description: ex.Message) };
            }
        }

        public async Task<ErrorOr<OutBoundMessageResponse>> SendAsync(SendMessageBase message)
        {
            try
			{
                var jsonObj = JsonConvert.SerializeObject(message);

                var payload = new StringContent(jsonObj, Encoding.UTF8, "application/json");
                var temp = await payload.ReadAsStringAsync();
                var response = await httpClient.PostAsync("messages", payload);

                var stringResponse = await response.Content.ReadAsStringAsync();
                logger.LogInformation("Response from whatsapp Post::{stringResponse}", stringResponse);

                response.EnsureSuccessStatusCode();

                return JsonConvert.DeserializeObject<OutBoundMessageResponse>(stringResponse)!;
            }
			catch (Exception ex)
			{
				return new Error[] { Error.Failure(description: ex.Message) };
			}
        }
    }
}
