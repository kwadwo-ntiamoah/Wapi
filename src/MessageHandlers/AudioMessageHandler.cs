using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wapi.src.IncomingMessageModels;

namespace Wapi.src.MessageHandlers
{
    internal class AudioMessageHandler : IMessageHandler<AudioMessage, string>
    {
        public MessageResponse<string> HandleMessage(AudioMessage message)
        {
            return new MessageResponse<string>
            {
                Type = message.Type,
                From = message.From,
                Id = message.Id,
                Content = message.Audio.Id
            };
        }
    }
}
