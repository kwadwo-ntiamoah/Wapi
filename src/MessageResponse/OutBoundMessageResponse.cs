using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wapi.src.IncomingMessageModels;

namespace Wapi.src.MessageResponse
{
    public class OutBoundMessageResponse
    {
        [JsonProperty("messaging_product")]
        public string MessagingProduct { get; set; } = string.Empty;

        [JsonProperty("contacts")]
        public List<ResponseContact> Contacts { get; set; } = [];

        [JsonProperty("messages")]
        public List<ResponseMessage> Messages { get; set; } = [];
    }

    public class ResponseContact
    {
        [JsonProperty("input")]
        public string Input { get; set; } = string.Empty;

        [JsonProperty("wa_id")]
        public string WaId { get; set; } = string.Empty;
    }

    public class ResponseMessage
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;
    }
}
