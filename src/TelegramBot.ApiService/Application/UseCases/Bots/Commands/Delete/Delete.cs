using Ardalis.GuardClauses;
using TelegramBot.ApiService.Application.Common.Interfaces;
using TelegramBot.ApiService.Domain.Events;

namespace TelegramBot.ApiService.Application.UseCases.Bots.Commands.Delete;

public record DeleteBotCommand(int Id) : IRequest<bool>;

public class DeleteBotCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteBotCommand, bool>
{
    public async Task<bool> Handle(DeleteBotCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Bots.FindAsync([request.Id], cancellationToken);
        Guard.Against.NotFound(request.Id, entity);

        entity.AddDomainEvent(new BotDeletedEvent(entity));
        context.Bots.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
