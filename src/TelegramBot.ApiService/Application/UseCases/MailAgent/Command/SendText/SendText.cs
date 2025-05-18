using Ardalis.GuardClauses;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using TelegramBot.ApiService.Application.Common.Interfaces;

namespace TelegramBot.ApiService.Application.UseCases.MailAgent.Command.SendText;

public class SendTextCommand : IRequest
{
    public int BotId { get; set; }
    public string Text { get; set; } = null!;
}

public class SendTextCommandHandler(IApplicationDbContext context, IBotLifecycleService botLifecycleService)
    : IRequestHandler<SendTextCommand>
{
    public async Task Handle(SendTextCommand request, CancellationToken cancellationToken)
    {
        var bot = await context.Bots.FindAsync([request.BotId], cancellationToken);
        Guard.Against.NotFound(request.BotId, bot);

        var botClient = botLifecycleService.GetBot(bot.Id);
        var chats = await context.Chats.ToListAsync(cancellationToken);

        foreach (var chat in chats)
        {
            try
            {
                await botClient.SendMessage(
                    chatId: chat.ChatId,
                    text: request.Text,
                    parseMode: ParseMode.MarkdownV2,
                    cancellationToken: cancellationToken
                );
            }
            catch
            {
                // ignored
            }
        }
    }
}
