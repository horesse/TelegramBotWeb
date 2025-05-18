using TelegramBot.ApiService.Application.Common.Interfaces;
using TelegramBot.ApiService.Domain.Events;

namespace TelegramBot.ApiService.Application.UseCases.Bots.EventHandlers;

public class BotDeletedEventHandler(IBotLifecycleService service) : INotificationHandler<BotDeletedEvent>
{
    public async Task Handle(BotDeletedEvent notification, CancellationToken cancellationToken)
    {
        await service.StopBotAsync(notification.Item.Id);
    }
}
