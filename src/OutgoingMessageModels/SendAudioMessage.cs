using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wapi.src.OutgoingMessageModels
{
    public class SendAudioMessage: SendMessageBase
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "audio";

        [JsonProperty("audio")]
        public required SendAudio Audio { get; set; }
    }

    public class SendAudio
    {
        /// <summary>
        /// Provide Id when resource is uploaded using cloud api
        /// Otherwise provide link to your own resource
        /// </summary>
        [JsonProperty("id")]
        public required string Id { get; set; }
    }
}
