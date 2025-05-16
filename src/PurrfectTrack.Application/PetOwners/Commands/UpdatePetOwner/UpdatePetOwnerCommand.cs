// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        UpdatePetOwnerCommand
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.PetOwners.Commands.UpdatePetOwner;

public record UpdatePetOwnerCommand(Guid Id, string FirstName, string LastName, string? PhoneNumber, string? Address, DateTime? DateOfBirth, string? Gender)
    : ICommand<UpdatePetOwnerResult>;

public record UpdatePetOwnerResult(Guid Id);

public class UpdatePetOwnerCommandValidator : AbstractValidator<UpdatePetOwnerCommand>
{
    public UpdatePetOwnerCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Firstname is required");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Lastname is required");
    }
}