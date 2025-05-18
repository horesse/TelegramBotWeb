using Telegram.Bot;
using TelegramBot.ApiService.Domain.Entities;

namespace TelegramBot.ApiService.Application.Common.Interfaces;

public interface IBotLifecycleService
{
    Task StartBotAsync(Bot bot);
    Task StopBotAsync(int botId);
    ITelegramBotClient GetBot(int botId);
}
