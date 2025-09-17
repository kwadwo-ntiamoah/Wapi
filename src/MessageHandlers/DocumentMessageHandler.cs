using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wapi.src.IncomingMessageModels;

namespace Wapi.src.MessageHandlers
{
    internal class DocumentMessageHandler : IMessageHandler<DocumentMessage, Document>
    {
        public MessageResponse<Document> HandleMessage(DocumentMessage message)
        {
            return new MessageResponse<Document>
            {
                Type = message.Type,
                Id = message.Id,
                From = message.From,
                Content = message.Document
            };
        }
    }
}
