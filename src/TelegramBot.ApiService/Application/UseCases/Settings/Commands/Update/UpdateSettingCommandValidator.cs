namespace TelegramBot.ApiService.Application.UseCases.Settings.Commands.Update;

public class UpdateSettingCommandValidator : AbstractValidator<UpdateSettingCommand>
{
    public UpdateSettingCommandValidator()
    {
        RuleFor(v => v.Key)
            .NotEmpty();
    }
}
