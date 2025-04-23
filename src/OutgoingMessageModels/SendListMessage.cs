using Newtonsoft.Json;
using System.Collections.Generic;

namespace Wapi.src.OutgoingMessageModels
{
    public class SendListMessage: SendMessageBase
    {

        [JsonProperty("type")]
        public string Type { get; set; } = "interactive";

        [JsonProperty("interactive")]
        public required SendListInteractive Interactive { get; set; }
    }

    public class SendListInteractive
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "list";

        [JsonProperty("header")]
        public SendListHeader? Header { get; set; }

        [JsonProperty("body")]
        public SendListBody? Body { get; set; }

        [JsonProperty("footer")]
        public SendListFooter? Footer { get; set; }

        [JsonProperty("action")]
        public SendListAction? Action { get; set; }
    }

    public class SendListHeader
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "text";

        [JsonProperty("text")]
        public string Text { get; set; } = string.Empty;
    }

    public class SendListBody
    {
        [JsonProperty("text")]
        public string Text { get; set; } = string.Empty;
    }

    public class SendListFooter
    {
        [JsonProperty("text")]
        public string Text { get; set; } = string.Empty;
    }

    public class SendListAction
    {
        [JsonProperty("button")]
        public string Button { get; set; } = string.Empty;

        [JsonProperty("sections")]
        public List<SendListSection> Sections { get; set; } = [];
    }

    public class SendListSection
    {
        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;

        [JsonProperty("rows")]
        public List<SendListRow> Rows { get; set; } = [];
    }

    public class SendListRow
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;
    }

}

