using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wapi.src.IncomingMessageModels;

namespace Wapi.src.MessageHandlers
{
    internal class TextMessageHandler : IMessageHandler<TextMessage, string>
    {
        public MessageResponse<string> HandleMessage(TextMessage message)
        {
            return new MessageResponse<string>
            {
                Type = message.Type,
                Id = message.Id,
                From = message.From,
                Content = message.Text.Body
            };
        }
    }
}
