using Wapi.src.IncomingMessageModels;

namespace Wapi.src.MessageHandlers
{
    internal class StickerMessageHandler : IMessageHandler<StickerMessage, Sticker>
    {
        public MessageResponse<Sticker> HandleMessage(StickerMessage message)
        {
            return new MessageResponse<Sticker>
            {
                Type = message.Type,
                Id = message.Id,
                From = message.From,
                Content = message.Sticker
            };
        }
    }
}
