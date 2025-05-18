using Ardalis.GuardClauses;
using TelegramBot.ApiService.Application.Common.Interfaces;
using TelegramBot.ApiService.Application.Common.Models;

namespace TelegramBot.ApiService.Application.UseCases.Settings.Queries.GetSetting;

public record GetSettingQuery(int Id) : IRequest<SettingDto>;

public class GetSettingQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetSettingQuery, SettingDto>
{
    public async Task<SettingDto> Handle(GetSettingQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.Settings.FindAsync([request.Id], cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        
        return mapper.Map<SettingDto>(entity);
    }
}
