using FluentValidation;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Companies.Commands.DeleteCompany;

public record DeleteCompanyCommand(Guid Id)
    : ICommand<DeleteCompanyResult>;

public record DeleteCompanyResult(bool IsSuccess);

public class DeleteCompanyCommandValidator : AbstractValidator<DeleteCompanyCommand>
{
    public DeleteCompanyCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Name is required");
    }
}