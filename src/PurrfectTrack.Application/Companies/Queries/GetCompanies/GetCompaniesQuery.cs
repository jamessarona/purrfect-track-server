// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetCompaniesQuery
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Companies.Queries.GetCompanies;

public record GetCompaniesQuery(PaginationRequest PaginationRequest)
    : IQuery<GetCompaniesResult>;

public record GetCompaniesResult(PaginatedResult<CompanyModel> Companies);