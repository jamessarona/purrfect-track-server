// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        DeleteVetStaffCommand
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.VetStaffs.Commands.DeleteVetStaff;

public record DeleteVetStaffCommand(Guid Id) : ICommand<DeleteVetStaffResult>;

public record DeleteVetStaffResult(bool IsSuccess);

public class DeleteVetStaffCommandValidator : AbstractValidator<DeleteVetStaffCommand>
{
    public DeleteVetStaffCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}