// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetVetStaffsByCompanyCommand
//  Created:     5/17/2025 4:52:26 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.VetStaffs.Queries.GetVetStaffsByCompany;

public record GetVetStaffsByCompanyQuery(Guid CompanyId)
    : IQuery<GetVetStaffsByCompanyResult>;

public record GetVetStaffsByCompanyResult(List<VetStaffModel> VetStaffs);