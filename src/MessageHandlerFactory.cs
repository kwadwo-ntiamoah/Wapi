using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wapi.src.IncomingMessageModels;
using Wapi.src.MessageHandlers;

namespace Wapi.src
{
    public class MessageHandlerFactory
    {
        private readonly Dictionary<Type, object> _handlers = new();

        public MessageHandlerFactory()
        {
            _handlers[typeof(TextMessage)] = new TextMessageHandler();
        }

        public IMessageHandler<TMessage, TResponse>? GetHandler<TMessage, TResponse>() where TMessage: BaseMessage
        {
            if (_handlers.TryGetValue(typeof(TMessage), out var handler))
            {
                return handler as IMessageHandler<TMessage, TResponse>;
            }

            return null;
        }
    }
}
