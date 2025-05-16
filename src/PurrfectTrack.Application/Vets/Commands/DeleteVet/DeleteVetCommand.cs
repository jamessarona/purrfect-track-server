// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        DeleteVetCommand
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Vets.Commands.DeleteVet;

public record DeleteVetCommand(Guid Id) : ICommand<DeleteVetResult>;

public record DeleteVetResult(bool IsSuccess);

public class DeleteVetCommandValidator : AbstractValidator<DeleteVetCommand>
{
    public DeleteVetCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}