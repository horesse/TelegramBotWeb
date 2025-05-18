namespace TelegramBot.ApiService.Domain.Events;

public class BotDeletedEvent(Bot item) : BaseEvent
{
    public Bot Item { get; } = item;
}
