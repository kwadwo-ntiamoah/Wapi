using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wapi.src.OutgoingMessageModels
{
    public class SendImageMessage: SendMessageBase
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "image";

        [JsonProperty("image")]
        public required SendImage Image { get; set; }
    }

    public class SendImage
    {
        /// <summary>
        /// Id of resource from cloud media api
        /// </summary>
        [JsonProperty("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Provide link to your own resource if not using cloud media api
        /// </summary>
        [JsonProperty("link")]
        public string? Link { get; set; }

        /// <summary>
        /// Image caption text.
        /// Maximum 1024 characters.
        /// </summary>
        [MaxLength(1024)]
        [JsonProperty("caption")]
        public required string Caption { get; set; }
    }
}
