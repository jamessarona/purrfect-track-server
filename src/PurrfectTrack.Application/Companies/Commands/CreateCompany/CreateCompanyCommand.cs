using FluentValidation;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Companies.Commands.CreateCompany;

public record CreateCompanyCommand(string Name, string? Description, string? PhoneNumber, string? Email, string? Website, string? Address, string TaxpayerId, bool IsActive)
    : ICommand<CreateCompanyResult>;

public record CreateCompanyResult(Guid Id);

public class CreateCompanyCommandValidator: AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
    }
}