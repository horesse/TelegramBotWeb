using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using TelegramBot.ApiService.Application.UseCases.MailAgent.Command.SendText;
using TelegramBot.ApiService.Infrastructure;

namespace TelegramBot.ApiService.Endpoints;

public class MailAgent : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(SendText, "SendText")
            ;
    }

    public async Task<NoContent> SendText(ISender sender, SendTextCommand command)
    {
        await sender.Send(command);
        return TypedResults.NoContent();
    }
}
