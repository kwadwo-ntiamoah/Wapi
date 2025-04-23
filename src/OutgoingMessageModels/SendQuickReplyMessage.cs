using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wapi.src.OutgoingMessageModels
{
    public class SendQuickReplyMessage: SendMessageBase
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "interactive";

        [JsonProperty("interactive")]
        public required SendQuickReply Interactive { get; set; }
    }

    public class SendQuickReply
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "button";

        [JsonProperty("header")]
        public SendQuickReplyHeader? Header { get; set; }

        [JsonProperty("body")]
        public SendQuickReplyBody? Body { get; set; }

        [JsonProperty("footer")]
        public SendQuickReplyFooter? Footer { get; set; }

        [JsonProperty("action")]
        public required SendQuickReplyAction Action { get; set; }
    }

    public abstract class SendQuickReplyHeader { }

    public class SendQuickReplyImageHeader: SendQuickReplyHeader
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "image";

        [JsonProperty("image")]
        public SendQuickReplyImage? Image { get; set; }
    }

    public class SendQuickReplyImage
    {
        //public string? Id { get; set; }
        [JsonProperty("link")]
        public string? Link { get; set; }
    }

    public class SendQuickReplyTextHeader: SendQuickReplyHeader
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "text";

        [JsonProperty("text")]
        public string Text { get; set; } = string.Empty;
    }

    public class SendQuickReplyBody
    {
        [JsonProperty("text")]
        public string Text { get; set; } = string.Empty;
    }

    public class SendQuickReplyFooter
    {
        [JsonProperty("text")]
        public string Text { get; set; } = string.Empty;
    }

    public class SendQuickReplyAction
    {
        [JsonProperty("buttons")]
        public List<SendQuickReplyActionButton> Buttons { get; set; } = [];
    }

    public class SendQuickReplyActionButton
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "reply";

        [JsonProperty("reply")]
        public required SendQuickReplyActionButtonReply Reply { get; set; }
    }

    public class SendQuickReplyActionButtonReply
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;
    }
}
