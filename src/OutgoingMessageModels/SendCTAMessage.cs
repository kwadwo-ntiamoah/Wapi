using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wapi.src.OutgoingMessageModels
{
    public class SendCTAMessage: SendMessageBase
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "interactive";

        [JsonProperty("interactive")]
        public required SendCTAInteractive Interactive { get; set; }
    }

    public class SendCTAInteractive
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "cta_url";

        [JsonProperty("header")]
        public SendCTAInteractiveHeader? Header { get; set; }

        [JsonProperty("body")]
        public SendCTAInteractiveBody? Body { get; set; }

        [JsonProperty("footer")]
        public SendCTAInteractiveFooter? Footer { get; set; }

        [JsonProperty("action")]
        public required SendCTAAction Action { get; set; }
    }

    public class SendCTAInteractiveHeader
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "text";

        [JsonProperty("text")]
        public required string Text { get; set; }
    }

    public class SendCTAInteractiveBody
    {
        [JsonProperty("text")]
        public required string Text { get; set; }
    }

    public class SendCTAInteractiveFooter
    {
        [JsonProperty("text")]
        public required string Text { get; set; }
    }

    public class SendCTAAction
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "cta_url";

        [JsonProperty("parameters")]
        public required SendCTAActionParameters Parameters { get; set; }
    }

    public class SendCTAActionParameters
    {
        [JsonProperty("display_text")]
        public required string DisplayText { get; set; }

        [JsonProperty("url")]
        public required string Url { get; set; }
    }
}
