using FluentValidation;
using PurrfectTrack.Shared.CQRS;
using System.Windows.Input;

namespace PurrfectTrack.Application.Companies.Commands.UpdateCompany;

public record UpdateCompanyCommand(Guid Id, string Name, string? Description, string? PhoneNumber, string? Email, string? Website, string? Address, string TaxpayerId, bool IsActive)
    : ICommand<UpdateCompanyResult>;

public record UpdateCompanyResult(Guid Id);

public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
{
    public UpdateCompanyCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
    }
}