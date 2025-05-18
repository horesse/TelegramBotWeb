namespace TelegramBot.ApiService.Domain.Entities;

public class ChatMailingList
{
    public long ChatId { get; set; }
    public long MailingListId { get; set; }
    public DateTime AddedAt { get; set; }

    // Навигационные свойства
    public Chat Chat { get; set; } = null!;
    public MailingList MailingList { get; set; } = null!;
}
