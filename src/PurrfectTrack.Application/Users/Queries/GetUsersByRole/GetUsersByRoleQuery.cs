// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetUsersByRoleQuery
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Users.Queries.GetUsersByRole;

public record GetUsersByRoleQuery(string Role, PaginationRequest PaginationRequest)
    : IQuery<GetUsersByRoleResult>;

public record GetUsersByRoleResult(PaginatedResult<UserModel> Users);