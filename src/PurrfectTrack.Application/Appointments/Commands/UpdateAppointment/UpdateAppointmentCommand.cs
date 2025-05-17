// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        UpdateAppointmentCommand
//  Created:     5/17/2025 7:54:11 PM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Appointments.Commands.UpdateAppointment;

public record UpdateAppointmentCommand(Guid Id, string Title, string Description, DateTime StartDate, DateTime EndDate, string Location,
    Guid PetOwnerId, Guid PetId, Guid VetId, Guid? VetStaffId, AppointmentStatus Status, string Notes, Guid CompanyId)
    : ICommand<UpdateAppointmentResult>;

public record UpdateAppointmentResult(Guid Id);

public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
{
    public UpdateAppointmentCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("StartDate is required");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("EndDate is required");

        RuleFor(x => x.PetOwnerId)
            .NotEmpty().WithMessage("PetOwnerId is required");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required");
    }
}