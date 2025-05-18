using TelegramBot.ApiService.Application.Common.Interfaces;
using TelegramBot.ApiService.Application.Common.Models;
using TelegramBot.ApiService.Domain.Entities;
using TelegramBot.ApiService.Domain.Events;

namespace TelegramBot.ApiService.Application.UseCases.Settings.Commands.Create;

public class CreateSettingCommand : IRequest<SettingDto>
{
    public string Key { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string? Description { get; set; }
}

public class CreateSettingCommandHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<CreateSettingCommand, SettingDto>
{
    public async Task<SettingDto> Handle(CreateSettingCommand request, CancellationToken cancellationToken)
    {
        var entity = new Setting
        {
            Key = request.Key, Token = request.Token, Description = request.Description, UpdatedAt = DateTime.UtcNow
        };
        
        entity.AddDomainEvent(new SettingCreatedEvent(entity));
        context.Settings.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        
        return mapper.Map<SettingDto>(entity);
    }
}
