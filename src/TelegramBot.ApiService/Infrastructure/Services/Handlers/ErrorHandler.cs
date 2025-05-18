using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace TelegramBot.ApiService.Infrastructure.Services.Handlers;

public class ErrorHandler(ILogger logger)
{
    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Ошибка при получении обновлений");
        return Task.CompletedTask;
    }
}

