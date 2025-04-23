using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Wapi.src.OutgoingMessageModels;
using System.Reflection.PortableExecutable;

namespace Wapi.src.IncomingMessageModels
{
    public abstract class BaseMessage
    {
        [JsonProperty("context")]
        public Context? Context { get; set; }

        [JsonProperty("from")]
        public string From { get; set; } = string.Empty;

        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
    }

    public abstract class InteractiveBaseMessage: BaseMessage { }

    public class Context
    {
        [JsonProperty("from")]
        public string From { get; set; } = string.Empty;

        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;
    }

    public class BaseMessageConverter : JsonConverter
    {
        private readonly string[] allowedTypes = ["text", "image", "location", "document", "audio", "interactive", "sticker", "reaction"];
        private readonly string[] allowedInteractiveTypes = ["nfm_reply", "list_reply", "button_reply"];
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(BaseMessage);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            try
            {
                // read json object
                var jObject = JObject.Load(reader);

                // get type
                var typeValue = jObject["type"]?.ToString().ToLowerInvariant();

                if (string.IsNullOrEmpty(typeValue) || !allowedTypes.Contains(typeValue))
                {
                    throw new JsonSerializationException($"Invalid or missing type: {typeValue}");
                }

                // create appropriate BaseMessage based on type
                BaseMessage? message = typeValue switch
                {
                    "text" => JsonConvert.DeserializeObject<TextMessage>(jObject.ToString()),
                    "audio" => JsonConvert.DeserializeObject<AudioMessage>(jObject.ToString()),
                    "image" => JsonConvert.DeserializeObject<ImageMessage>(jObject.ToString()),
                    "document" => JsonConvert.DeserializeObject<DocumentMessage>(jObject.ToString()),
                    "location" => JsonConvert.DeserializeObject<LocationMessage>(jObject.ToString()),
                    "interactive" => GetInteractiveMessageType(jObject),
                    "sticker" => JsonConvert.DeserializeObject<StickerMessage>(jObject.ToString()),
                    "reaction" => JsonConvert.DeserializeObject<ReactionMessage>(jObject.ToString()),
                    _ => throw new JsonSerializationException($"Unexpected type {typeValue}")
                };

                return message;
            }
            catch (Exception ex)
            {
                throw new JsonSerializationException("Error deserializing object", ex);
            }
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        private BaseMessage? GetInteractiveMessageType(JObject jObject)
        {
            // get type
            var interactiveValue = jObject["interactive"]!;
            var typeValue = interactiveValue["type"]?.ToString().ToLowerInvariant();

            if (string.IsNullOrEmpty(typeValue) || !allowedInteractiveTypes.Contains(typeValue))
            {
                throw new JsonSerializationException($"Invalid or missing type: {typeValue}");
            }

            // create appropriate BaseMessage based on type
            BaseMessage? message = typeValue switch
            {
                "nfm_reply" => JsonConvert.DeserializeObject<InteractiveFlowMessage>(jObject.ToString()),
                "list_reply" => JsonConvert.DeserializeObject<InteractiveListMessage>(jObject.ToString()),
                "button_reply" => JsonConvert.DeserializeObject<InteractiveQuickReplyMessage>(jObject.ToString()),
                _ => throw new JsonSerializationException($"Unexpected type {typeValue}")
            };

            return message;
        }
    }
}