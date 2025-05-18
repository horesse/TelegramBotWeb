using TelegramBot.ApiService.Domain.Events;

namespace TelegramBot.ApiService.Application.UseCases.Settings.EventHandlers;

public class SettingDeletedEventHandler : INotificationHandler<SettingDeletedEvent>
{
    public Task Handle(SettingDeletedEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
