using FluentValidation;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Vets.Commands.UpdateVet;

public record UpdateVetCommand(Guid Id, string FirstName, string LastName, string? PhoneNumber, string? Address, DateTime? DateOfBirth, string? Gender,
        string? LicenseNumber, DateTime? LicenseExpiryDate, string? Specialization, int? YearsOfExperience, string? ClinicName, string? ClinicAddress, DateTime? EmploymentDate)
    : ICommand<UpdateVetResult>;

public record UpdateVetResult(Guid Id);

public class UpdateVetCommandValidator : AbstractValidator<UpdateVetCommand>
{
    public UpdateVetCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Firstname is required");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Lastname is required");
    }
}