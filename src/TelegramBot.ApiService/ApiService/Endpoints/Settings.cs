using MediatR;
using TelegramBot.ApiService.Application.UseCases.Settings.Commands.Create;
using TelegramBot.ApiService.Application.UseCases.Settings.Commands.Delete;
using TelegramBot.ApiService.Application.UseCases.Settings.Commands.Update;
using TelegramBot.ApiService.Application.UseCases.Settings.Queries.GetSetting;
using TelegramBot.ApiService.Application.UseCases.Settings.Queries.GetSettings;
using TelegramBot.ApiService.Infrastructure;

namespace TelegramBot.ApiService.Endpoints;

public class Settings : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetSettings)
            .MapGet(GetSettingById, "{id}")
            .MapPost(CreateSettings)
            .MapPut(UpdateSettingsById, "{id}")
            .MapDelete(DeleteSettingsById, "{id}")
            ;
    }

    public async Task<IResult> GetSettings(ISender sender)
    {
        var result = await sender.Send(new GetSettingsQuery());
        return Results.Ok(result);
    }

    public async Task<IResult> GetSettingById(ISender sender, int id)
    {
        var result = await sender.Send(new GetSettingQuery(id));
        return Results.Ok(result);
    }

    public async Task<IResult> CreateSettings(ISender sender, CreateSettingCommand command)
    {
        var result = await sender.Send(command);
        return Results.Ok(result);
    }
    
    public async Task<IResult> UpdateSettingsById(ISender sender, UpdateSettingCommand command)
    {
        var result = await sender.Send(command);
        return Results.Ok(result);
    }

    public async Task<IResult> DeleteSettingsById(ISender sender, int id)
    {
        var result = await sender.Send(new DeleteSettingCommand(id));
        return Results.Ok(result);
    }
}
