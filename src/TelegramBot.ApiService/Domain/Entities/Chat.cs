namespace TelegramBot.ApiService.Domain.Entities;

public class Chat : BaseEntity
{
    public long ChatId { get; set; }
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? LastInteractionAt { get; set; }
    
    public int BotId { get; set; }
    public Bot Bot { get; set; } = null!;

    public ICollection<MessageDelivery> MessageDeliveries { get; set; } = new List<MessageDelivery>();
    public ICollection<ChatMailingList> ChatMailingLists { get; set; } = new List<ChatMailingList>();
}
