using FluentValidation;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Pets.Commands.CreatePet;

public record CreatePetCommand(Guid PetOwnerId, string Name, string? Species, string? Breed, string? Gender, DateTime? DateOfBirth, float? Weight, string? Color, bool IsNeutered)
    : ICommand<CreatePetResult>;

public record CreatePetResult(Guid Id);

public class CreatePetCommandValidator : AbstractValidator<CreatePetCommand>
{
    public CreatePetCommandValidator()
    {
        RuleFor(x => x.PetOwnerId).NotNull().WithMessage("Pet Owner is required");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
    }
}