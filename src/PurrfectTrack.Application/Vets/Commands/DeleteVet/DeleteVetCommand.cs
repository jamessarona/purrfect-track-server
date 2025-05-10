using FluentValidation;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Vets.Commands.DeleteVet;

public record DeleteVetCommand(Guid Id) : ICommand<DeleteVetResult>;

public record DeleteVetResult(bool IsSuccess);

public class DeleteVetCommandValidator : AbstractValidator<DeleteVetCommand>
{
    public DeleteVetCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}