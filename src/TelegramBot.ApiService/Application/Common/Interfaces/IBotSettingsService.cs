using TelegramBot.ApiService.Domain.Entities;

namespace TelegramBot.ApiService.Application.Common.Interfaces;

public interface IBotSettingsService
{
    Task<IEnumerable<Bot>> GetAllActiveBotSettingsAsync(CancellationToken cancellationToken);
    Task<Bot?> GetBotSettingAsync(int settingId, CancellationToken cancellationToken);
}
