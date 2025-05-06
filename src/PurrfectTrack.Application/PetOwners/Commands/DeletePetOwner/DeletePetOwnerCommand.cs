using FluentValidation;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.PetOwners.Commands.DeletePetOwner;

public record DeletePetOwnerCommand(Guid Id)
    : ICommand<DeletePetOwnerResult>;

public record DeletePetOwnerResult(bool IsSuccess);

public class DeletePetOwnerCommandValidator : AbstractValidator<DeletePetOwnerCommand>
{
    public DeletePetOwnerCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}