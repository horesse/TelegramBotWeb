using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.ApiService.Application.Common.Interfaces;

namespace TelegramBot.ApiService.Infrastructure.Services.Handlers;

public class UpdateHandler(ILogger logger, IServiceScopeFactory scopeFactory)
{
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        
        var chatId = update.Message?.Chat.Id;
        Guard.Against.Null(chatId, nameof(chatId));

        var chat = await context.Chats.FirstOrDefaultAsync(c => c.ChatId == chatId.Value, cancellationToken);
        
        if (chat == null)
        {
            var entity = new TelegramBot.ApiService.Domain.Entities.Chat
            {
                ChatId = chatId.Value,
                Username = update.Message?.Chat.Username,
                FirstName = update.Message?.Chat.FirstName,
                LastName = update.Message?.Chat.LastName,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                LastInteractionAt = DateTime.UtcNow
            };
            
            context.Chats.Add(entity);
        }
        else
        {
            chat.LastInteractionAt = DateTime.UtcNow;
        }

        await context.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Get message with Text: {Text}", update.Message?.Text);
    }
}

