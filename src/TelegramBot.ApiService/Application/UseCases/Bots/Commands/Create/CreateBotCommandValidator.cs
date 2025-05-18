namespace TelegramBot.ApiService.Application.UseCases.Bots.Commands.Create;

public class CreateBotCommandValidator : AbstractValidator<CreateBotCommand>
{
    public CreateBotCommandValidator()
    {
        RuleFor(v => v.Key)
            .NotEmpty();
    }
}
