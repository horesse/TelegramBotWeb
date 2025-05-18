using TelegramBot.ApiService.Domain.Common;

namespace TelegramBot.ApiService.Domain.Entities;

public class MailingList : BaseEntity
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }

    // Навигационные свойства
    public ICollection<ChatMailingList> ChatMailingLists { get; set; } = new List<ChatMailingList>();
}

