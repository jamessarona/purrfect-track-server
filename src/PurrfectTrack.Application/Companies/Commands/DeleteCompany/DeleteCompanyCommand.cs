// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        DeleteCompanyCommand
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

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