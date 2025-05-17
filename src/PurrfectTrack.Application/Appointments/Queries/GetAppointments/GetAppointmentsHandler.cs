// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetAppointmentsHandler
//  Created:     5/17/2025 8:04:34 PM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Appointments.Queries.GetAppointments;

public class GetAppointmentsHandler
    : BaseQueryHandler, IQueryHandler<GetAppointmentsQuery, GetAppointmentsResult>
{
    public GetAppointmentsHandler(IApplicationDbContext dbContext, IMapper mapper) 
        : base(dbContext, mapper)
    {
    }

    public async Task<GetAppointmentsResult> Handle(GetAppointmentsQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var appointmentQuery = dbContext.Appointments.AsQueryable();

        var totalCount = await appointmentQuery.CountAsync(cancellationToken);

        var appointments = await appointmentQuery
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var appointmentsModels = mapper.Map<List<AppointmentModel>>(appointments);

        var paginatedResult = new PaginatedResult<AppointmentModel>(
            pageIndex,
            pageSize,
            totalCount,
            appointmentsModels
        );

        return new GetAppointmentsResult(paginatedResult);
    }
}