using Ardalis.GuardClauses;
using TelegramBot.ApiService.Application.Common.Interfaces;
using TelegramBot.ApiService.Domain.Events;

namespace TelegramBot.ApiService.Application.UseCases.Settings.Commands.Delete;

public record DeleteSettingCommand(int Id) : IRequest<bool>;

public class DeleteSettingCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteSettingCommand, bool>
{
    public async Task<bool> Handle(DeleteSettingCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Settings.FindAsync([request.Id], cancellationToken);
        Guard.Against.NotFound(request.Id, entity);

        entity.AddDomainEvent(new SettingDeletedEvent(entity));
        context.Settings.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
