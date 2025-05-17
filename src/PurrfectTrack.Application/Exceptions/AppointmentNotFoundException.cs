// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        AppointmentNotFoundException
//  Created:     5/17/2025 7:58:17 PM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Exceptions;

public class AppointmentNotFoundException : NotFoundException
{
    public AppointmentNotFoundException(Guid id) : base("Appointment", id)
    {
    }
}