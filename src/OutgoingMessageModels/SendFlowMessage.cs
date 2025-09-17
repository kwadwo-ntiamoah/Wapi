using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using Wapi.src.IncomingMessageModels;
using Wapi.src.OutgoingMessageModels;

namespace Wapi.src.OutgoingMessageModels
{
    public class SendFlowMessage : SendMessageBase
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "interactive";

        [JsonProperty("interactive")]
        public required SendFlowInteractive Interactive { get; set; }
    }

    public class SendFlowInteractive
    {
        /// <summary>
        /// Value must be flow
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = "flow";

        [JsonProperty("header")]
        public SendFlowMessageHeader? Header { get; set; }

        [JsonProperty("body")]
        public SendFlowMessageBody? Body { get; set; }

        [JsonProperty("footer")]
        public SendFlowMessageFooter? Footer { get; set; }

        [JsonProperty("action")]
        public SendFlowInteractiveAction? Action { get; set; }
    }

    public class SendFlowMessageHeader
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "text";

        [JsonProperty("text")]
        public string Text { get; set; } = string.Empty;
    }

    public class SendFlowMessageBody
    {
        [JsonProperty("text")]
        public string Text { get; set; } = string.Empty;
    }

    public class SendFlowMessageFooter
    {
        [JsonProperty("text")]
        public string Text { get; set; } = string.Empty;
    }

    public class SendFlowInteractiveAction
    {
        /// <summary>
        /// Value must be flow
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = "flow";

        [JsonProperty("parameters")]
        public SendFlowActionParameters? Parameters { get; set; }
    }

    public class SendFlowActionParameters
    {
        [JsonProperty("flow_message_version")]
        public string FlowMessageVersion { get; set; } = "3";

        /// <summary>
        /// Whatsapp Cloud Auth Token
        /// </summary>
        [JsonProperty("flow_token")]
        public required string FlowToken { get; set; }

        /// <summary>
        /// Unique ID of the Flow provided by WhatsApp.
        /// Cannot be used with the flow_name parameter.Only one of these parameters is required.
        /// </summary>
        [JsonProperty("flow_id")]
        public required string FlowId { get; set; }

        /// <summary>
        /// Text on the CTA button. For example: "Signup"
        /// CTA text length is advised to be 30 characters or less(no emoji).
        /// </summary>
        [MaxLength(30)]
        [JsonProperty("flow_cta")]
        public required string FlowCta { get; set; } = "Proceed";

        /// <summary>
        /// navigate or data_exchange
        /// Default value is navigate
        /// navigate for static screens and data_exchange to receive dynamic data
        /// </summary>
        [JsonProperty("flow_action")]
        public string FlowAction { get; set; } = "navigate";

        /// <summary>
        /// Optional if flow_action is navigate. Should be omitted otherwise
        /// ie Remove if action is navigate
        /// </summary>
        [JsonProperty("flow_action_payload")]
        public SendFlowActionPayload? FlowActionPayload { get; set; }
    }

    public class SendFlowActionPayload
    {
        /// <summary>
        /// The id of the first screen
        /// </summary>
        [JsonProperty("screen")]
        public required string Screen { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, object>? Data { get; set; } = new();
    }
}
