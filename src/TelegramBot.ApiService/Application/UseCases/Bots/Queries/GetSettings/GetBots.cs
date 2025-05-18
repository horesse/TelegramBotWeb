using TelegramBot.ApiService.Application.Common.Interfaces;
using TelegramBot.ApiService.Application.Common.Models;

namespace TelegramBot.ApiService.Application.UseCases.Bots.Queries.GetSettings;

public class GetBotsQuery : IRequest<List<BotDto>>;

public class GetBotsQueryHandler(IApplicationDbContext dbContext, IMapper mapper) : IRequestHandler<GetBotsQuery, List<BotDto>>
{
    public async Task<List<BotDto>> Handle(GetBotsQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Bots
            .ProjectTo<BotDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
