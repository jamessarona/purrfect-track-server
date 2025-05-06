using FluentValidation;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Pets.Commands.UpdatePet;

public record UpdatePetCommand(Guid Id, Guid PetOwnerId, string? Name, string? Species, string? Breed, string? Gender, DateTime? DateOfBirth, float? Weight, string? Color, bool? IsNeutered)
    : ICommand<UpdatePetResult>;

public record UpdatePetResult(Guid Id);

public class UpdatePetCommandValidator : AbstractValidator<UpdatePetCommand>
{
    public UpdatePetCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.PetOwnerId).NotEmpty().WithMessage("OwnerId is required.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
    }
}