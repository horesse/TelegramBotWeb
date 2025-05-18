using TelegramBot.ApiService.Domain.Entities;

namespace TelegramBot.ApiService.Application.Common.Models;

public class SettingDto
{
    public string Key { get; set; } = null!;
    public string? Value { get; set; }
    public string? Description { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Setting, SettingDto>();
        }
    }
}
