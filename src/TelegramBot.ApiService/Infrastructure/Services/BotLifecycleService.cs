using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using TelegramBot.ApiService.Application.Common.Interfaces;
using TelegramBot.ApiService.Domain.Entities;
using TelegramBot.ApiService.Infrastructure.Handlers;

namespace TelegramBot.ApiService.Infrastructure.Services;

public class BotLifecycleService(ILogger<BotLifecycleService> logger) : IBotLifecycleService
{
    private readonly ConcurrentDictionary<int, ITelegramBotClient> _activeBots = new();

    public Task StartBotAsync(Setting setting)
    {
        Guard.Against.NullOrWhiteSpace(setting.Value);
        var bot = new TelegramBotClient(setting.Value);
        if (!_activeBots.TryAdd(setting.Id, bot))
        {
            return Task.CompletedTask;
        }

        // Создаем обработчики для сообщений
        var updateHandler = new UpdateHandler(logger);
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
