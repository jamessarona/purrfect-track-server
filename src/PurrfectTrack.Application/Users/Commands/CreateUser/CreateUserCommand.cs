using FluentValidation;
using PurrfectTrack.Domain.Enums;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Users.Commands.CreateUser;

public record CreateUserCommand(string Email, string Password, UserRole Role)
  : ICommand<CreateUserResult>;

public record CreateUserResult(Guid Id);

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Invalid user role.");

        RuleFor(x => x.Password)
            .Matches("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$")
            .When(x => !string.IsNullOrEmpty(x.Password))
            .WithMessage("Password must be at least 8 characters long and contain at least one letter and one number.");
    }
}
