namespace TelegramBot.ApiService.Domain.Events;

public class SettingCreatedEvent(Setting item) : BaseEvent
{
    public Setting Item { get; } = item;
}
