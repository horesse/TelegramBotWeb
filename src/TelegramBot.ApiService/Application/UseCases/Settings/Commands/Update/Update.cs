using Ardalis.GuardClauses;
using TelegramBot.ApiService.Application.Common.Interfaces;
using TelegramBot.ApiService.Application.Common.Models;

namespace TelegramBot.ApiService.Application.UseCases.Settings.Commands.Update;

public class UpdateSettingCommand : IRequest<SettingDto>
{
    public int Id { get; set; }
    public string Key { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string? Description { get; set; }
}

public class UpdateSettingCommandHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<UpdateSettingCommand, SettingDto>
{
    public async Task<SettingDto> Handle(UpdateSettingCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Settings.FindAsync([request.Id], cancellationToken);
        Guard.Against.NotFound(request.Key, entity);
        
        entity.Key = request.Key;
        entity.Token = request.Token;
        entity.Description = request.Description;
        entity.UpdatedAt = DateTime.UtcNow;
        
        context.Settings.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
        
        return mapper.Map<SettingDto>(entity);
    }
}
