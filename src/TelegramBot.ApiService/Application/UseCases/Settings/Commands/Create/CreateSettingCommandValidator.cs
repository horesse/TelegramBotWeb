namespace TelegramBot.ApiService.Application.UseCases.Settings.Commands.Create;

public class CreateSettingCommandValidator : AbstractValidator<CreateSettingCommand>
{
    public CreateSettingCommandValidator()
    {
        RuleFor(v => v.Key)
            .NotEmpty();
    }
}
