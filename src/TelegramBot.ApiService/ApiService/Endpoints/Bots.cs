using MediatR;
using TelegramBot.ApiService.Application.UseCases.Bots.Commands.Create;
using TelegramBot.ApiService.Application.UseCases.Bots.Commands.Delete;
using TelegramBot.ApiService.Application.UseCases.Bots.Commands.Update;
using TelegramBot.ApiService.Application.UseCases.Bots.Queries.GetBot;
using TelegramBot.ApiService.Application.UseCases.Bots.Queries.GetSettings;
using TelegramBot.ApiService.Infrastructure;

namespace TelegramBot.ApiService.Endpoints;

public class Bots : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetBots)
            .MapGet(GetBotById, "{id}")
            .MapPost(CreateBot)
            .MapPut(UpdateBotById, "{id}")
            .MapDelete(DeleteBotById, "{id}")
            ;
    }

    public async Task<IResult> GetBots(ISender sender)
    {
        var result = await sender.Send(new GetBotsQuery());
        return Results.Ok(result);
    }

    public async Task<IResult> GetBotById(ISender sender, int id)
    {
        var result = await sender.Send(new GetBotQuery(id));
        return Results.Ok(result);
    }

    public async Task<IResult> CreateBot(ISender sender, CreateBotCommand command)
    {
        var result = await sender.Send(command);
        return Results.Ok(result);
    }
    
    public async Task<IResult> UpdateBotById(ISender sender, UpdateBotCommand command)
    {
        var result = await sender.Send(command);
        return Results.Ok(result);
    }

    public async Task<IResult> DeleteBotById(ISender sender, int id)
    {
        var result = await sender.Send(new DeleteBotCommand(id));
        return Results.Ok(result);
    }
}
