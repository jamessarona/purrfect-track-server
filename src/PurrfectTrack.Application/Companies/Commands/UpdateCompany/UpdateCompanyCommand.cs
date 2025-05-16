// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        UpdateCompanyCommand
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

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