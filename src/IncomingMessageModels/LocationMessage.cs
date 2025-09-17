using Newtonsoft.Json;

namespace Wapi.src.IncomingMessageModels
{
    public class LocationMessage : BaseMessage
    {
        [JsonProperty("location")]
        public required Location Location { get; set; }
    }

    public class Location
    {
        [JsonProperty("address")]
        public string? Address { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }
}
