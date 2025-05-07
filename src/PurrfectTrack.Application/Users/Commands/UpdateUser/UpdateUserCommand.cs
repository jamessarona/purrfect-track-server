using FluentValidation;
using PurrfectTrack.Domain.Enums;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(Guid Id, string Email, string? Password, UserRole Role, bool IsActive) 
    : ICommand<UpdateUserResult>;

public record UpdateUserResult(Guid Id);

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(100)
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Invalid role specified.");

        When(x => !string.IsNullOrWhiteSpace(x.Password), () =>
        {
            RuleFor(x => x.Password!)
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .Matches("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$")
                .WithMessage("Password must contain at least one letter and one number.");
        });
    }
}