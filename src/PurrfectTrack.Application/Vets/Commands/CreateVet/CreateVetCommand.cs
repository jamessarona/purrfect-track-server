using FluentValidation;
using PurrfectTrack.Application.PetOwners.Commands.CreatePetOwner;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Vets.Commands.CreateVet;

public record CreateVetCommand(string Email, string Password, string FirstName, string LastName, string? PhoneNumber, string? Address, DateTime? DateOfBirth, string? Gender,
        string? LicenseNumber, DateTime? LicenseExpiryDate, string? Specialization, int? YearsOfExperience, string? ClinicName, string? ClinicAddress, DateTime? EmploymentDate, Guid? CompanyId)
    : ICommand<CreateVetResult>;

public record CreateVetResult(Guid Id);

public class CreateVetCommandValidation : AbstractValidator<CreateVetCommand>
{
    public CreateVetCommandValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Password)
            .Matches("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$")
            .When(x => !string.IsNullOrEmpty(x.Password))
            .WithMessage("Password must be at least 8 characters long and contain at least one letter and one number.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Firstname is required");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Lastname is required");
    }
}