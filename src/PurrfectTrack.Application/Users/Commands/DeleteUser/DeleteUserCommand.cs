using FluentValidation;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id) : ICommand<DeleteUserResult>;

public record DeleteUserResult(bool IsSuccess);

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("User Id is required.");
    }
}