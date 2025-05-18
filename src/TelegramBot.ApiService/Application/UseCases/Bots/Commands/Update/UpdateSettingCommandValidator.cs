namespace TelegramBot.ApiService.Application.UseCases.Bots.Commands.Update;

public class UpdateBotCommandValidator : AbstractValidator<UpdateBotCommand>
{
    public UpdateBotCommandValidator()
    {
        RuleFor(v => v.Key)
            .NotEmpty();
    }
}
