// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetVetsByCompanyQuery
//  Created:     5/17/2025 4:41:14 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Vets.Queries.GetVetsByCompany;

public record GetVetsByCompanyQuery(Guid CompanyId)
    : IQuery<GetVetsByCompanyResult>;

public record GetVetsByCompanyResult(List<VetModel> Vets);