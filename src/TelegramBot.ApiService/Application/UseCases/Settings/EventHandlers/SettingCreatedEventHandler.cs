using TelegramBot.ApiService.Domain.Events;

namespace TelegramBot.ApiService.Application.UseCases.Settings.EventHandlers;

public class SettingCreatedEventHandler : INotificationHandler<SettingCreatedEvent>
{
    public Task Handle(SettingCreatedEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
