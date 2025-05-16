// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetVetStaffByIdQuery
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.VetStaffs.Queries.GetVetStaffById;

public record GetVetStaffByIdQuery(Guid Id) : IQuery<GetVetStaffByIdResult>;

public record GetVetStaffByIdResult(VetStaffModel VetStaff);