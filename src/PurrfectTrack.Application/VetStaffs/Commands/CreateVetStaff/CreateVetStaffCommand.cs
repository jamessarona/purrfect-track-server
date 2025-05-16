// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        CreateVetStaffCommand
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.VetStaffs.Commands.CreateVetStaff;

public record CreateVetStaffCommand(string Email, string Password, string FirstName, string LastName, string? PhoneNumber, string? Address, DateTime? DateOfBirth, string? Gender,
        string? Position, string? Department, DateTime? EmploymentDate, Guid? CompanyId)
    : ICommand<CreateVetStaffResult>;

public record CreateVetStaffResult(Guid Id);

public class CreateVetStaffCommandValidation : AbstractValidator<CreateVetStaffCommand>
{
    public CreateVetStaffCommandValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Password)
            .Matches("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$")
            .When(x => !string.IsNullOrEmpty(x.Password))
            .WithMessage("Password must be at least 8 characters long and contain at least one letter and one number.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Firstname is required");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Lastname is required");
    }
}