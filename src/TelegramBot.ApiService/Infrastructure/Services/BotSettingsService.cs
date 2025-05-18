using Microsoft.EntityFrameworkCore;
using TelegramBot.ApiService.Application.Common.Interfaces;
using TelegramBot.ApiService.Domain.Entities;

namespace TelegramBot.ApiService.Infrastructure.Services;

public class BotSettingsService(IApplicationDbContext context) : IBotSettingsService
{
    public async Task<IEnumerable<Bot>> GetAllActiveBotSettingsAsync(CancellationToken cancellationToken)
    {
        return await context.Bots
            .ToListAsync(cancellationToken);
    }

    public async Task<Bot?> GetBotSettingAsync(int settingId, CancellationToken cancellationToken)
    {
        return await context.Bots.FindAsync([settingId], cancellationToken);
    }
}

