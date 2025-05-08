using FluentValidation;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Users.Commands.Login;

public record LoginCommand(string Email, string Password) : ICommand<LoginResult>;

public record LoginResult(string Token, Guid SessionId, string RefreshToken);

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}