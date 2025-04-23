using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wapi.src.OutgoingMessageModels
{
    public class SendLocationMessage: SendMessageBase
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "location";

        [JsonProperty("location")]
        public required SendLocation Location { get; set; }
    }

    public class SendLocation
    {
        [JsonProperty("latitude")]
        public required string Latitude { get; set; }

        [JsonProperty("longitude")]
        public required string Longitude { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("address")]
        public string? Address { get; set; }
    }
}
