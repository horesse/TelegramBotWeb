namespace TelegramBot.ApiService.Domain.Entities;

public class MessageDelivery : BaseEntity
{
    public int MessageId { get; set; }
    public int ChatId { get; set; }
    public DeliveryStatus Status { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime? SentAt { get; set; }
    public DateTime? DeliveredAt { get; set; }

    // Навигационные свойства
    public Message Message { get; set; } = default!;
    public Chat Chat { get; set; } = default!;
}
