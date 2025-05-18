using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.ApiService.Infrastructure.Handlers;

public class UpdateHandler(ILogger logger)
{
    public Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        logger.LogInformation("Получено обновление типа: {UpdateType}", update.Type);
        return Task.CompletedTask;
    }
}

