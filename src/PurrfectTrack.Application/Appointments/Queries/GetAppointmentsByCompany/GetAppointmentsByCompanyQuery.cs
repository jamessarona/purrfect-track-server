// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetAppointmentsByCompanyQuery
//  Created:     5/17/2025 8:10:54 PM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Appointments.Queries.GetAppointmentsByCompany;

public record GetAppointmentsByCompanyQuery(Guid CompanyId)
    : IQuery<GetAppointmentsByCompanyResult>;

public record GetAppointmentsByCompanyResult(List<AppointmentModel> Appointments);