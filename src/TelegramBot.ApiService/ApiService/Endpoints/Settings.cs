using MediatR;
using TelegramBot.ApiService.Application.UseCases.Settings.Queries.GetSettings;
using TelegramBot.ApiService.Infrastructure;

namespace TelegramBot.ApiService.Endpoints;

public class Settings : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetSettings);
    }

    public async Task<IResult> GetSettings(ISender sender)
    {
        var result = await sender.Send(new GetSettingsQuery());
        return Results.Ok(result);
    }
}
