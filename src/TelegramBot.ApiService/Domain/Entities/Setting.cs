namespace TelegramBot.ApiService.Domain.Entities;

public class Setting : BaseEntity
{
    public string Key { get; set; } = null!;
    public string? Value { get; set; }
    public string? Description { get; set; }
    public DateTime UpdatedAt { get; set; }
}

