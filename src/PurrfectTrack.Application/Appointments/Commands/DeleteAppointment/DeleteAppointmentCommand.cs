// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        DeleteAppointmentCommand
//  Created:     5/17/2025 5:32:42 PM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Appointments.Commands.DeleteAppointment;

public record DeleteAppointmentCommand(Guid Id)
    : ICommand<DeleteAppointmentResult>;

public record DeleteAppointmentResult(bool IsSuccess);

public class DeleteAppointmentCommandValidator : AbstractValidator<DeleteAppointmentCommand>
{
    public DeleteAppointmentCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}