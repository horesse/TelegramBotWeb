using TelegramBot.ApiService.Application.Common.Interfaces;
using TelegramBot.ApiService.Application.Common.Models;

namespace TelegramBot.ApiService.Application.UseCases.Settings.Queries.GetSettings;

public class GetSettingsQuery : IRequest<List<SettingDto>>;

public class GetSettingsQueryHandler(IApplicationDbContext dbContext, IMapper mapper) : IRequestHandler<GetSettingsQuery, List<SettingDto>>
{
    public async Task<List<SettingDto>> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Settings
            .ProjectTo<SettingDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
