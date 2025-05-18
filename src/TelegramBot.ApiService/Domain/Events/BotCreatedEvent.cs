namespace TelegramBot.ApiService.Domain.Events;

public class BotCreatedEvent(Bot item) : BaseEvent
{
    public Bot Item { get; } = item;
}
