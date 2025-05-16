// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        UpdateVetCommand
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Vets.Commands.UpdateVet;

public record UpdateVetCommand(Guid Id, string FirstName, string LastName, string? PhoneNumber, string? Address, DateTime? DateOfBirth, string? Gender,
        string? LicenseNumber, DateTime? LicenseExpiryDate, string? Specialization, int? YearsOfExperience, string? ClinicName, string? ClinicAddress, DateTime? EmploymentDate, Guid? CompanyId)
    : ICommand<UpdateVetResult>;

public record UpdateVetResult(Guid Id);

public class UpdateVetCommandValidator : AbstractValidator<UpdateVetCommand>
{
    public UpdateVetCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Firstname is required");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Lastname is required");
    }
}