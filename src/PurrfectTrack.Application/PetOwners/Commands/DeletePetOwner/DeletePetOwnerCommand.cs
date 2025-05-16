// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        DeletePetOwnerCommand
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.PetOwners.Commands.DeletePetOwner;

public record DeletePetOwnerCommand(Guid Id)
    : ICommand<DeletePetOwnerResult>;

public record DeletePetOwnerResult(bool IsSuccess);

public class DeletePetOwnerCommandValidator : AbstractValidator<DeletePetOwnerCommand>
{
    public DeletePetOwnerCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}