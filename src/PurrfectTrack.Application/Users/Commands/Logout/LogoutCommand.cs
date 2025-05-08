using FluentValidation;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Users.Commands.Logout;

public record LogoutCommand(string Token) : ICommand;

public record LogoutResult(bool Success);

public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
{
    public LogoutCommandValidator()
    {
        RuleFor(x => x.Token).NotEmpty().WithMessage("Token is required.");
    }
}