using FluentValidation;
using FluentValidation.Validators;
using PurrfectTrack.Shared.CQRS;
using System.ComponentModel.DataAnnotations;
namespace PurrfectTrack.Application.PetOwners.Commands.CreatePetOwner;

public record CreatePetOwnerCommand(Guid UserId, string FirstName, string LastName, string? PhoneNumber, string? Address, DateTime? DateOfBirth, string? Gender)
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
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required");
    }
}