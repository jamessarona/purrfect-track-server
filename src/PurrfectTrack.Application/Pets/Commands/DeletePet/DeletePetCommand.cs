using FluentValidation;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Pets.Commands.DeletePet;

public record DeletePetCommand(Guid Id) 
    : ICommand<DeletePetResult>;

public record DeletePetResult(bool IsSuccess);

public class DeletePetCommandValidator : AbstractValidator<DeletePetCommand>
{
    public DeletePetCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}