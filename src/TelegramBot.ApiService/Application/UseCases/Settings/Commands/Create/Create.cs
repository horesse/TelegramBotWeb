using TelegramBot.ApiService.Application.Common.Interfaces;
using TelegramBot.ApiService.Application.Common.Models;
using TelegramBot.ApiService.Domain.Entities;

namespace TelegramBot.ApiService.Application.UseCases.Settings.Commands.Create;

public class CreateSettingCommand : IRequest<SettingDto>
{
    public string Key { get; set; } = null!;
    public string? Value { get; set; }
    public string? Description { get; set; }
}

public class CreateSettingCommandHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<CreateSettingCommand, SettingDto>
{
    public async Task<SettingDto> Handle(CreateSettingCommand request, CancellationToken cancellationToken)
    {
        var setting = new Setting
        {
            Key = request.Key, Value = request.Value, Description = request.Description, UpdatedAt = DateTime.UtcNow
        };
        
        context.Settings.Add(setting);
        await context.SaveChangesAsync(cancellationToken);
        
        return mapper.Map<SettingDto>(setting);
    }
}
