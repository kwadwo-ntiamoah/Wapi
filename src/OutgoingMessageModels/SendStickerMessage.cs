using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wapi.src.OutgoingMessageModels
{
    public class SendStickerMessage: SendMessageBase
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "sticker";

        [JsonProperty("sticker")]
        public required SendSticker Sticker { get; set; }
    }

    public class SendSticker
    {
        /// <summary>
        /// using uploaded media from Whatsapp cloud api
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;
    }
}
