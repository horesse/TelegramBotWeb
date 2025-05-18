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

    public Task StartBotAsync(Bot bot)
    {
        Guard.Against.NullOrWhiteSpace(bot.Token);
        var client = new TelegramBotClient(bot.Token);
        if (!_activeBots.TryAdd(bot.Id, client))
        {
            return Task.CompletedTask;
        }

        // Создаем обработчики для сообщений
        var updateHandler = new UpdateHandler(logger, scopeFactory, bot.Id);
        var errorHandler = new ErrorHandler(logger);

        // Запускаем бота с обработчиками
        client.StartReceiving(
            updateHandler: updateHandler.HandleUpdateAsync,
            errorHandler: errorHandler.HandlePollingErrorAsync,
            receiverOptions: new ReceiverOptions { AllowedUpdates = [] },
            cancellationToken: CancellationToken.None
        );

        logger.LogInformation("Bot {SettingId} started successfully", bot.Id);

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
