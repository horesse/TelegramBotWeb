using Ardalis.GuardClauses;
using TelegramBot.ApiService.Application.Common.Interfaces;
using TelegramBot.ApiService.Application.Common.Models;

namespace TelegramBot.ApiService.Application.UseCases.Bots.Commands.Update;

public class UpdateBotCommand : IRequest<BotDto>
{
    public int Id { get; set; }
    public string Key { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string? Description { get; set; }
}

public class UpdateBotCommandHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<UpdateBotCommand, BotDto>
{
    public async Task<BotDto> Handle(UpdateBotCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Bots.FindAsync([request.Id], cancellationToken);
        Guard.Against.NotFound(request.Key, entity);
        
        entity.Key = request.Key;
        entity.Token = request.Token;
        entity.Description = request.Description;
        entity.UpdatedAt = DateTime.UtcNow;
        
        context.Bots.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
        
        return mapper.Map<BotDto>(entity);
    }
}
