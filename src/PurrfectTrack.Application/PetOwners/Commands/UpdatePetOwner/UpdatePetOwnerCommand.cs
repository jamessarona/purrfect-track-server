using FluentValidation;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.PetOwners.Commands.UpdatePetOwner;

public record UpdatePetOwnerCommand(Guid Id, string FirstName, string LastName, string? PhoneNumber, string? Address, DateTime? DateOfBirth, string? Gender)
    : ICommand<UpdatePetOwnerResult>;

public record UpdatePetOwnerResult(Guid Id);

public class UpdatePetOwnerCommandValidator : AbstractValidator<UpdatePetOwnerCommand>
{
    public UpdatePetOwnerCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Firstname is required");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Lastname is required");
    }
}