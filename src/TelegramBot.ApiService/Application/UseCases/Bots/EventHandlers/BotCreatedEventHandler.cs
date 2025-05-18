using TelegramBot.ApiService.Application.Common.Interfaces;
using TelegramBot.ApiService.Domain.Events;

namespace TelegramBot.ApiService.Application.UseCases.Bots.EventHandlers;

public class BotCreatedEventHandler(IBotLifecycleService service)
    : INotificationHandler<BotCreatedEvent>
{
    public async Task Handle(BotCreatedEvent notification, CancellationToken cancellationToken)
    {
        await service.StartBotAsync(notification.Item);
    }
}
