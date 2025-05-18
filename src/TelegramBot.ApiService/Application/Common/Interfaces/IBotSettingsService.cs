using TelegramBot.ApiService.Domain.Entities;

namespace TelegramBot.ApiService.Application.Common.Interfaces;

public interface IBotSettingsService
{
    Task<IEnumerable<Setting>> GetAllActiveBotSettingsAsync(CancellationToken cancellationToken);
    Task<Setting?> GetBotSettingAsync(int settingId, CancellationToken cancellationToken);
}
