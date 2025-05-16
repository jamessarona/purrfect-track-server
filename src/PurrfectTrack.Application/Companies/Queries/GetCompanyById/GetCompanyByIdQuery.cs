// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetCompanyByIdQuery
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Companies.Queries.GetCompanyById;

public record GetCompanyByIdQuery(Guid Id) : IQuery<GetCompanyByIdResult>;

public record GetCompanyByIdResult(CompanyModel Company);