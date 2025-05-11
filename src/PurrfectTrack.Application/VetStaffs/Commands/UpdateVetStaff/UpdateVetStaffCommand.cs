using FluentValidation;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.VetStaffs.Commands.UpdateVetStaff;

public record UpdateVetStaffCommand(Guid Id, string FirstName, string LastName, string? PhoneNumber, string? Address, DateTime? DateOfBirth, string? Gender,
        string? Position, string? Department, DateTime? EmploymentDate)
    : ICommand<UpdateVetStaffResult>;

public record UpdateVetStaffResult(Guid Id);

public class UpdateVetStaffCommandValidator : AbstractValidator<UpdateVetStaffCommand>
{
    public UpdateVetStaffCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Firstname is required");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Lastname is required");
    }
}