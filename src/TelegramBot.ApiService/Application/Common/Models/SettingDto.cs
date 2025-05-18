using TelegramBot.ApiService.Domain.Entities;

namespace TelegramBot.ApiService.Application.Common.Models;

public class SettingDto
{
    public int Id { get; set; }
    public string Key { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Setting, SettingDto>()
                .ForMember(c => c.Token, opt => opt.Ignore())
                ;
        }
    }
}
