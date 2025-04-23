using Wapi.src.IncomingMessageModels;

namespace Wapi.src.MessageHandlers
{
    internal class ReactionMessageHandler : IMessageHandler<ReactionMessage, Reaction>
    {
        public MessageResponse<Reaction> HandleMessage(ReactionMessage message)
        {
            return new MessageResponse<Reaction>
            {
                Type = message.Type, 
                Id = message.Id,
                From = message.From,
                Content = message.Reaction
            };
        }
    }
}
