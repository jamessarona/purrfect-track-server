using FluentValidation;
using PurrfectTrack.Domain.Enums;
using PurrfectTrack.Shared.CQRS;
using System.ComponentModel.DataAnnotations;
namespace PurrfectTrack.Application.PetOwners.Commands.CreatePetOwner;

public record CreatePetOwnerCommand(string Email, string Password, string FirstName, string LastName, string? PhoneNumber, string? Address, DateTime? DateOfBirth, string? Gender)
    : ICommand<CreatePetOwnerResult>;

public record CreatePetOwnerResult(Guid Id);

public class CreatePetOwnerCommandValidator : AbstractValidator<CreatePetOwnerCommand>
{
    public CreatePetOwnerCommandValidator()
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