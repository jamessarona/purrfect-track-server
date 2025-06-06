﻿// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        AppointmentStatus
//  Created:     5/16/2025 6:28:24 PM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Domain.Enums;

public enum AppointmentStatus
{
    Pending,
    Confirmed,
    Cancelled,
    Ongoing,
    Completed
}