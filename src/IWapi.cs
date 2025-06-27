using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wapi.src.IncomingMessageModels;
using ErrorOr;
using Wapi.src.MessageResponse;
using Wapi.src.OutgoingMessageModels;
using Microsoft.AspNetCore.Http;

namespace Wapi.src
{
    public interface IWApi
    {
        public ErrorOr<string> ValidateInboundMessage(IQueryCollection queries);

        public ErrorOr<(string?, BaseMessage)> DecodeInboundMessage(string payload);

        /// <summary>
        /// Send a loading indicator
        /// </summary>
        /// <returns></returns>
        public Task<ErrorOr<bool>> ShowLoadingIndicator(string messageId);

        /// <summary>
        /// Send an Audio Message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendAudio message);

        /// <summary>
        /// Send a contact card
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendContact message);

        /// <summary>
        /// Send Multiple contact cards
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, List<SendContact> message);

        /// <summary>
        /// Send a CTA message with buttons (URL, Call)
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendCTAInteractive message);

        /// <summary>
        /// Send Documents
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendDocument message);

        /// <summary>
        /// Send an Interactive Flow Message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendFlowInteractive message);

        /// <summary>
        /// Send an Image
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendImage message);

        /// <summary>
        /// Send an interactive message from buttons
        /// Maximum of 10 buttons
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendListInteractive message);

        /// <summary>
        /// Send Location
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendLocation message);

        /// <summary>
        /// Send an interactive message with Quick Buttons
        /// Maxium of 3 buttons
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendQuickReply message);

        /// <summary>
        /// React to a message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendReaction message);

        /// <summary>
        /// Acknowledge receipt of a message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendReadReceipt message);

        /// <summary>
        /// Send a sticker as a message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendSticker message);

        /// <summary>
        /// Send text message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendText message);

        /// <summary>
        /// Send Video message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendVideo message);
        
        
    }
}
