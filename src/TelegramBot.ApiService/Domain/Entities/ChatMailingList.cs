namespace TelegramBot.ApiService.Domain.Entities;

public class ChatMailingList
{
    public int ChatId { get; set; }
    public int MailingListId { get; set; }
    public DateTime AddedAt { get; set; }

    // Навигационные свойства
    public Chat Chat { get; set; } = null!;
    public MailingList MailingList { get; set; } = null!;
}
