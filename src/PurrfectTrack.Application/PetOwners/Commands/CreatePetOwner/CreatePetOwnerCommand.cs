using FluentValidation;
using FluentValidation.Validators;
using PurrfectTrack.Shared.CQRS;
using System.ComponentModel.DataAnnotations;
namespace PurrfectTrack.Application.PetOwners.Commands.CreatePetOwner;

public record CreatePetOwnerCommand(string FirstName, string LastName, string Email, string? PhoneNumber, string? Address, DateTime? DateOfBirth, string? Gender)
    : ICommand<CreatePetOwnerResult>;

public record CreatePetOwnerResult(Guid Id);

public class CreatePetOwnerCommandValidator : AbstractValidator<CreatePetOwnerCommand>
{
    public CreatePetOwnerCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Firstname is required");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Lastname is required");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");
    }
}