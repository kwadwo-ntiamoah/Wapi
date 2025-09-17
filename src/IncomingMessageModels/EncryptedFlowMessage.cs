using Newtonsoft.Json;
namespace Wapi.src.IncomingMessageModels
{
    public class EncryptedFlowMessage
    {
        [JsonProperty("encrypted_aes_key")]
        public string EncryptedAesKey { get; set; } = string.Empty;

        [JsonProperty("encrypted_flow_data")]
        public string EncryptedFlowData { get; set; } = string.Empty;

        [JsonProperty("initial_vector")]
        public string InitialVector { get; set; } = string.Empty;
    }

    public class DecryptedFlowMessage
    {
        [JsonProperty("version")]   
        public string? Version { get; set; }

        [JsonProperty("action")]
        public string? Action { get; set; }

        [JsonProperty("screen")]
        public string? Screen { get; set; }

        [JsonProperty("data")]
        public dynamic? Data { get; set; }

        [JsonProperty("flow_token")]
        public string? FlowToken { get; set; }  
    }
}
