using Telegram.Bot;
using TelegramBot.ApiService.Domain.Entities;

namespace TelegramBot.ApiService.Application.Common.Interfaces;

public interface IBotLifecycleService
{
    Task StartBotAsync(Setting setting);
    Task StopBotAsync(int botId);
    ITelegramBotClient GetBot(int botId);
}
