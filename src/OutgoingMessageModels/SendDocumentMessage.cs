using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wapi.src.OutgoingMessageModels
{
    public class SendDocumentMessage: SendMessageBase
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "document";

        [JsonProperty("document")]
        public required SendDocument Document { get; set; }
    }

    public class SendDocument
    {
        /// <summary>
        /// Media ID when using uploaded media
        /// </summary>
        [JsonProperty("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Link to resource when linking to your own media
        /// </summary>
        [JsonProperty("link")]
        public string? Link { get; set; }

        /// <summary>
        /// Required - Document caption text
        /// </summary>
        [JsonProperty("caption")]
        public required string Caption { get; set; }

        /// <summary>
        /// Required - Document filename, with extension. The WhatsApp client will use an appropriate file type icon based on the extension
        /// </summary>
        [JsonProperty("filename")]
        public required string Filename { get; set; }
    }
}
