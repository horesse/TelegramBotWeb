namespace TelegramBot.ApiService.Domain.Entities;

public class Setting : BaseEntity
{
    public string Key { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime UpdatedAt { get; set; }
}

