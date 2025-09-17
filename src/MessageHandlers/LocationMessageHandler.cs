using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wapi.src.IncomingMessageModels;

namespace Wapi.src.MessageHandlers
{
    internal class LocationMessageHandler : IMessageHandler<LocationMessage, Location>
    {
        public MessageResponse<Location> HandleMessage(LocationMessage message)
        {
            return new MessageResponse<Location>
            {
                Type = message.Type,
                Id = message.Id,
                From = message.From,
                Content = message.Location
            };
        }
    }
}
