// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetAppointmentByIdQuery
//  Created:     5/17/2025 8:07:21 PM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Appointments.Queries.GetAppointmentById;

public record GetAppointmentByIdQuery(Guid Id)
    : IQuery<GetAppointmentByIdResult>;

public record GetAppointmentByIdResult(AppointmentModel Appointment);