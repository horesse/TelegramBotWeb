using TelegramBot.ApiService.Domain.Common;
using TelegramBot.ApiService.Domain.Enums;

namespace TelegramBot.ApiService.Domain.Entities;

public class Message : BaseEntity
{
    public string? Title { get; set; }
    public string Content { get; set; } = null!;
    public MessageType MessageType { get; set; }
    public string? MediaUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ScheduledAt { get; set; }

    // Навигационные свойства
    public ICollection<MessageDelivery> MessageDeliveries { get; set; } = new List<MessageDelivery>();
}

