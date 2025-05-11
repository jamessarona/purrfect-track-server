using FluentValidation;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.VetStaffs.Commands.DeleteVetStaff;

public record DeleteVetStaffCommand(Guid Id) : ICommand<DeleteVetStaffResult>;

public record DeleteVetStaffResult(bool IsSuccess);

public class DeleteVetStaffCommandValidator : AbstractValidator<DeleteVetStaffCommand>
{
    public DeleteVetStaffCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}