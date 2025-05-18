using Microsoft.EntityFrameworkCore;
using TelegramBot.ApiService.Application.Common.Interfaces;
using TelegramBot.ApiService.Domain.Entities;

namespace TelegramBot.ApiService.Infrastructure.Services;

public class BotSettingsService(IApplicationDbContext context) : IBotSettingsService
{
    public async Task<IEnumerable<Setting>> GetAllActiveBotSettingsAsync(CancellationToken cancellationToken)
    {
        return await context.Settings
            .ToListAsync(cancellationToken);
    }

    public async Task<Setting?> GetBotSettingAsync(int settingId, CancellationToken cancellationToken)
    {
        return await context.Settings.FindAsync([settingId], cancellationToken);
    }
}

