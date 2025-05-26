using ErrorOr;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using Wapi.src.Extensions;
using Wapi.src.MessageResponse;
using Wapi.src.OutgoingMessageModels;

namespace Wapi.src.Http
{
    public class WhatsappClient(ILogger<WhatsappClient> logger, HttpClient httpClient, IOptions<WhatsappConfig> options)
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

        public async Task<ErrorOr<string>> GetMediaUrlAsync(string mediaId)
        {
            try
            {
                var baseUrl = options.Value.BaseUrl;
                var version = options.Value.ApiVersion;
                var accessToken = options.Value.AccessToken;

                var url = $"{baseUrl}/{version}/{mediaId}";

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.SendAsync(request);
                var responseJson = await response.Content.ReadAsStringAsync();

                logger.LogInformation("Received Url:{url} from media api", responseJson);

                response.EnsureSuccessStatusCode();

                var mediaObj = JsonConvert.DeserializeObject<MetaMedia>(responseJson);
                return mediaObj!.Url;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred retrieving media details from ID with message:{message}", ex.Message);
                return new Error[] { Error.Failure(description: "Error occurred retrieving media details") };
            }
        }

        public async Task<ErrorOr<string>> GetMediaBase64String(string mediaUrl)
        {
            try
            {
                var accessToken = options.Value.AccessToken;

                var request = new HttpRequestMessage(HttpMethod.Get, mediaUrl);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var binaryData = await response.Content.ReadAsByteArrayAsync();
                string base64String = Convert.ToBase64String(binaryData);

                return base64String;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred retrieving media details from URL with message:{message}", ex.Message);
                return new Error[] { Error.Failure(description: "Error occurred retrieving media") };
            }
        }

        public class MetaMedia
        {
            [JsonProperty("messaging_product")]
            public string MessagingProduct { get; set; } = string.Empty;

            [JsonProperty("url")]
            public string Url { get; set; } = string.Empty;

            [JsonProperty("mime_type")]
            public string MimeType { get; set; } = string.Empty;

            [JsonProperty("sha256")]
            public string Sha256 { get; set; } = string.Empty;

            [JsonProperty("file_size")]
            public string FileSize { get; set; } = string.Empty;

            [JsonProperty("id")]
            public string Id { get; set; } = string.Empty;
        }
    }
}
