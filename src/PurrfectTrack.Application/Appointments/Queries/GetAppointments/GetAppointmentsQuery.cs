// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetAppointmentsQuery
//  Created:     5/17/2025 8:02:36 PM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Appointments.Queries.GetAppointments;

public record GetAppointmentsQuery(PaginationRequest PaginationRequest)
    : IQuery<GetAppointmentsResult>;

public record GetAppointmentsResult(PaginatedResult<AppointmentModel> Appointments);