namespace TelegramBot.ApiService.Domain.Events;

public class SettingDeletedEvent(Setting item) : BaseEvent
{
    public Setting Item { get; } = item;
}
