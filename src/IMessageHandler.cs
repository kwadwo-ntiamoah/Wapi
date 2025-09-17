using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wapi.src.IncomingMessageModels;

namespace Wapi.src
{
    public interface IMessageHandler<TMessage, TResponse> where TMessage: BaseMessage
    {
        MessageResponse<TResponse> HandleMessage(TMessage message);
    }

    public class MessageResponse<T>
    {
        public string Type { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public T? Content { get; set; }
    }
}
