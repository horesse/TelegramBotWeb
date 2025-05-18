using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using TelegramBot.ApiService.Application.Common.Interfaces;
using TelegramBot.ApiService.Domain.Entities;
using TelegramBot.ApiService.Infrastructure.Services.Handlers;

namespace TelegramBot.ApiService.Infrastructure.Services;

public class BotLifecycleService(ILogger<BotLifecycleService> logger, IServiceScopeFactory scopeFactory)
    : IBotLifecycleService
{
    private readonly ConcurrentDictionary<int, ITelegramBotClient> _activeBots = new();

    public Task StartBotAsync(Setting setting)
    {
        Guard.Against.NullOrWhiteSpace(setting.Token);
        var bot = new TelegramBotClient(setting.Token);
        if (!_activeBots.TryAdd(setting.Id, bot))
        {
            return Task.CompletedTask;
        }

        // Создаем обработчики для сообщений
        var updateHandler = new UpdateHandler(logger, scopeFactory);
        var errorHandler = new ErrorHandler(logger);

        // Запускаем бота с обработчиками
        bot.StartReceiving(
            updateHandler: updateHandler.HandleUpdateAsync,
            errorHandler: errorHandler.HandlePollingErrorAsync,
            receiverOptions: new ReceiverOptions { AllowedUpdates = [] },
            cancellationToken: CancellationToken.None
        );

        logger.LogInformation("Bot {SettingId} started successfully", setting.Id);

        return Task.CompletedTask;
    }

    public ITelegramBotClient GetBot(int botId)
    {
        return _activeBots[botId];
    }
    
    public Task StopBotAsync(int botId)
    {
        if (_activeBots.TryRemove(botId, out var bot))
        {
            // Остановка бота
            logger.LogInformation("Bot {BotId} stopped successfully", botId);
        }

        return Task.CompletedTask;
    }
}
