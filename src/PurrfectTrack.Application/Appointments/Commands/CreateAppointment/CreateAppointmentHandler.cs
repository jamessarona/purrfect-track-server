// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        CreateAppointmentHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Appointments.Commands.CreateAppointment;

public class CreateAppointmentHandler
    : BaseHandler, ICommandHandler<CreateAppointmentCommand, CreateAppointmentResult>
{
    public CreateAppointmentHandler(IApplicationDbContext dbContext) 
        : base(dbContext) { }

    public async Task<CreateAppointmentResult> Handle(CreateAppointmentCommand command, CancellationToken cancellationToken)
    {
        var appointment = new Appointment(
            command.Title,
            command.Description,
            command.StartDate,
            command.EndDate,
            command.Location,
            command.CompanyId,
            command.PetOwnerId,
            command.PetId,
            command.VetId,
            command.VetStaffId,
            command.Status,
            command.Notes
            );

        dbContext.Appointments.Add(appointment);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateAppointmentResult(appointment.Id);
    }
}
