// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        CreateCompanyCommand
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

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