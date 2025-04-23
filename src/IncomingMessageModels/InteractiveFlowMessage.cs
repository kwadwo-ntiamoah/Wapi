using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wapi.src.IncomingMessageModels
{
    public class InteractiveFlowMessage: InteractiveBaseMessage
    {
        [JsonProperty("interactive")]
        public InteractiveFlow? Interactive { get; set; }
    }

    public class InteractiveFlow
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "nfm_reply";

        [JsonProperty("nfm_reply")]
        public NfmReply? NfmReply { get; set; }
    }

    public class NfmReply
    {
        [JsonProperty("response_json")]
        public string? ResponseJson { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; } = "Sent";

        [JsonProperty("name")]
        public string Name { get; set; } = "flow";
    }
}
