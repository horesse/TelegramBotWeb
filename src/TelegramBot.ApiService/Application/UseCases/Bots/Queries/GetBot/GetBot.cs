using Ardalis.GuardClauses;
using TelegramBot.ApiService.Application.Common.Interfaces;
using TelegramBot.ApiService.Application.Common.Models;

namespace TelegramBot.ApiService.Application.UseCases.Bots.Queries.GetBot;

public record GetBotQuery(int Id) : IRequest<BotDto>;

public class GetBotQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetBotQuery, BotDto>
{
    public async Task<BotDto> Handle(GetBotQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.Bots.FindAsync([request.Id], cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        
        return mapper.Map<BotDto>(entity);
    }
}
