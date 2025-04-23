using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wapi.src.IncomingMessageModels;

namespace Wapi.src.MessageHandlers
{
    internal class ImageMessageHandler : IMessageHandler<ImageMessage, Media>
    {
        public MessageResponse<Media> HandleMessage(ImageMessage message)
        {
            return new MessageResponse<Media>
            {
                Type = message.Type,
                Id = message.Id,
                From = message.From,
                Content = message.Image
            };
        }
    }
}
