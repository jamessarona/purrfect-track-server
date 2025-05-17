// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        UpdateAppointmentHandler
//  Created:     5/17/2025 7:54:38 PM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Appointments.Commands.UpdateAppointment;

public class UpdateAppointmentHandler
    : BaseHandler, ICommandHandler<UpdateAppointmentCommand, UpdateAppointmentResult>
{
    public UpdateAppointmentHandler(IApplicationDbContext dbContext) 
        : base(dbContext)
    {
    }

    public async Task<UpdateAppointmentResult> Handle(UpdateAppointmentCommand command, CancellationToken cancellationToken)
    {
        var appointment = await dbContext.Appointments
            .FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);

        if (appointment is null)
            throw new AppointmentNotFoundException(command.Id);

        appointment.Title = command.Title;
        appointment.Description = command.Description ?? appointment.Description;
        appointment.StartDate = command.StartDate;
        appointment.StartDate = command.StartDate;
        appointment.Location = command.Location ?? appointment.Location;
        appointment.CompanyId = command.CompanyId;
        appointment.PetOwnerId = command.PetOwnerId;
        appointment.PetId = command.PetId;
        appointment.VetId = command.VetId;
        appointment.VetStaffId = command.VetStaffId;
        appointment.Status = command.Status;
        appointment.Notes = command.Notes ?? appointment.Notes;

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateAppointmentResult(appointment.Id);
    }
}