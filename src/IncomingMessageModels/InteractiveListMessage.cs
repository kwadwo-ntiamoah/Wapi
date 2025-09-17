using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wapi.src.IncomingMessageModels
{
    public class InteractiveListMessage : InteractiveBaseMessage
    {
        [JsonProperty("interactive")]
        public InteractiveList? Interactive { get; set; }
    }

    public class InteractiveList
    {
        [JsonProperty("list_reply")]
        public ListReply? ListReply { get; set; }

    }

    public class ListReply
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;
    }
}
