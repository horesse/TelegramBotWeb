using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TelegramBot.ApiService.Application.Common.Interfaces;

namespace TelegramBot.ApiService.Infrastructure.Services;

public class BotInitializationService(
    IBotLifecycleService botLifecycleService,
    IServiceScopeFactory scopeFactory,
    ILogger<BotInitializationService> logger)
    : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            using var scope = scopeFactory.CreateScope();
            var botSettingsService = scope.ServiceProvider.GetRequiredService<IBotSettingsService>();
            
            var activeBots = await botSettingsService.GetAllActiveBotSettingsAsync(cancellationToken);
            foreach (var bot in activeBots)
            {
                await botLifecycleService.StartBotAsync(bot);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при инициализации ботов");
            throw;
        }
    }


    public Task StopAsync(CancellationToken cancellationToken)
    {
        // Здесь можно добавить логику остановки ботов при завершении работы приложения
        logger.LogInformation("Остановка всех ботов...");
        return Task.CompletedTask;
    }
}

