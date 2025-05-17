// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetAppointmentByIdHandler
//  Created:     5/17/2025 8:08:33 PM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Appointments.Queries.GetAppointmentById;

public class GetAppointmentByIdHandler
    : BaseQueryHandler, IQueryHandler<GetAppointmentByIdQuery, GetAppointmentByIdResult>
{
    public GetAppointmentByIdHandler(IApplicationDbContext dbContext, IMapper mapper) 
        : base(dbContext, mapper)
    {
    }

    public async Task<GetAppointmentByIdResult> Handle(GetAppointmentByIdQuery query, CancellationToken cancellationToken)
    {
        var appointment = await dbContext.Appointments
            .SingleOrDefaultAsync(c => c.Id == query.Id, cancellationToken);

        if (appointment is null)
            throw new AppointmentNotFoundException(query.Id);

        var appointmentModel = mapper.Map<AppointmentModel>(appointment);

        return new GetAppointmentByIdResult(appointmentModel);
    }
}