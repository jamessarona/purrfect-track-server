// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        UpdateVetStaffCommand
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.VetStaffs.Commands.UpdateVetStaff;

public record UpdateVetStaffCommand(Guid Id, string FirstName, string LastName, string? PhoneNumber, string? Address, DateTime? DateOfBirth, string? Gender,
        string? Position, string? Department, DateTime? EmploymentDate, Guid? CompanyId)
    : ICommand<UpdateVetStaffResult>;

public record UpdateVetStaffResult(Guid Id);

public class UpdateVetStaffCommandValidator : AbstractValidator<UpdateVetStaffCommand>
{
    public UpdateVetStaffCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Firstname is required");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Lastname is required");
    }
}