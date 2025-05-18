using TelegramBot.ApiService.Application.Common.Interfaces;
using TelegramBot.ApiService.Application.Common.Models;
using TelegramBot.ApiService.Domain.Entities;
using TelegramBot.ApiService.Domain.Events;

namespace TelegramBot.ApiService.Application.UseCases.Bots.Commands.Create;

public class CreateBotCommand : IRequest<BotDto>
{
    public string Key { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string? Description { get; set; }
}

public class CreateBotCommandHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<CreateBotCommand, BotDto>
{
    public async Task<BotDto> Handle(CreateBotCommand request, CancellationToken cancellationToken)
    {
        var entity = new Bot
        {
            Key = request.Key, Token = request.Token, Description = request.Description, UpdatedAt = DateTime.UtcNow
        };
        
        entity.AddDomainEvent(new BotCreatedEvent(entity));
        context.Bots.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        
        return mapper.Map<BotDto>(entity);
    }
}
