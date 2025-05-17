// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        DeleteAppointmentHandler
//  Created:     5/17/2025 5:35:34 PM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Appointments.Commands.DeleteAppointment;

public class DeleteAppointmentHandler
    : BaseHandler, ICommandHandler<DeleteAppointmentCommand, DeleteAppointmentResult>
{
    public DeleteAppointmentHandler(IApplicationDbContext dbContext) 
        : base(dbContext)
    {
    }

    public async Task<DeleteAppointmentResult> Handle(DeleteAppointmentCommand command, CancellationToken cancellationToken)
    {
        var appointment = await dbContext.Appointments
            .FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);

        if (appointment is null)
            return new DeleteAppointmentResult(false);

        dbContext.Appointments.Remove(appointment);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteAppointmentResult(true);
    }
}