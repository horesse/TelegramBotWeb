using TelegramBot.ApiService.Application.Common.Interfaces;
using TelegramBot.ApiService.Domain.Events;

namespace TelegramBot.ApiService.Application.UseCases.Settings.EventHandlers;

public class SettingCreatedEventHandler(IBotLifecycleService service)
    : INotificationHandler<SettingCreatedEvent>
{
    public async Task Handle(SettingCreatedEvent notification, CancellationToken cancellationToken)
    {
        await service.StartBotAsync(notification.Item);
    }
}
