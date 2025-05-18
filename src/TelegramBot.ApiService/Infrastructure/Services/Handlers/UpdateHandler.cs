using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.ApiService.Application.Common.Interfaces;

namespace TelegramBot.ApiService.Infrastructure.Services.Handlers;

public class UpdateHandler(ILogger logger, IServiceScopeFactory scopeFactory)
{
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

        if (update.Type is UpdateType.Message or UpdateType.MyChatMember)
        {
            var chatId = update.Message?.Chat.Id ?? update.MyChatMember?.Chat.Id;
            Guard.Against.Null(chatId, nameof(chatId));

            // Обработка входа пользователя в чат или его возвращения
            if (update.MyChatMember?.NewChatMember.Status is ChatMemberStatus.Member or ChatMemberStatus.Administrator
                or ChatMemberStatus.Creator)
            {
                var chat = await context.Chats.FirstOrDefaultAsync(c => c.ChatId == chatId.Value, cancellationToken);
                
                if (chat == null)
                {
                    var entity = new TelegramBot.ApiService.Domain.Entities.Chat
                    {
                        ChatId = chatId.Value,
                        Username = update.MyChatMember.From.Username,
                        FirstName = update.MyChatMember.From.FirstName,
                        LastName = update.MyChatMember.From.LastName,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        LastInteractionAt = DateTime.UtcNow
                    };
                    
                    context.Chats.Add(entity);
                    logger.LogInformation("User joined chat: {ChatId}", chatId);
                }
            }
            // Обработка выхода пользователя из чата
            else if (update.MyChatMember?.NewChatMember.Status is ChatMemberStatus.Left or ChatMemberStatus.Kicked)
            {
                var chat = await context.Chats.FirstOrDefaultAsync(c => c.ChatId == chatId.Value, cancellationToken);
                
                if (chat != null)
                {
                    context.Chats.Remove(chat);
                    logger.LogInformation("User left chat: {ChatId}", chatId);
                }
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
