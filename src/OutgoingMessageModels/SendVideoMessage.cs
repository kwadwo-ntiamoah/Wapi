using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wapi.src.OutgoingMessageModels
{
    public class SendVideoMessage : SendMessageBase
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "video";

        [JsonProperty("video")]
        public required SendVideo Video { get; set; }
    }

    public class SendVideo
    {
        [JsonProperty("link")]
        public required string Link { get; set; }

        [MaxLength(1024)]
        [JsonProperty("caption")]
        public required string Caption { get; set; }
    }
}
