using TelegramBot.ApiService.Application.Common.Interfaces;
using TelegramBot.ApiService.Domain.Events;

namespace TelegramBot.ApiService.Application.UseCases.Settings.EventHandlers;

public class SettingDeletedEventHandler(IBotLifecycleService service) : INotificationHandler<SettingDeletedEvent>
{
    public async Task Handle(SettingDeletedEvent notification, CancellationToken cancellationToken)
    {
        await service.StopBotAsync(notification.Item.Id);
    }
}
