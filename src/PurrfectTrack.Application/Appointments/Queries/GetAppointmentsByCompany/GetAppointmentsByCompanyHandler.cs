// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetAppointmentByCompanyHandler
//  Created:     5/17/2025 8:13:39 PM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Appointments.Queries.GetAppointmentsByCompany;

public class GetAppointmentsByCompanyHandler
    : BaseQueryHandler, IQueryHandler<GetAppointmentsByCompanyQuery, GetAppointmentsByCompanyResult>
{
    public GetAppointmentsByCompanyHandler(IApplicationDbContext dbContext, IMapper mapper) 
        : base(dbContext, mapper)
    {
    }

    public async Task<GetAppointmentsByCompanyResult> Handle(GetAppointmentsByCompanyQuery query, CancellationToken cancellationToken)
    {
        var appointments = await dbContext.Appointments
            .Where(v => v.CompanyId == query.CompanyId)
            .ProjectTo<AppointmentModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetAppointmentsByCompanyResult(appointments);
    }
}