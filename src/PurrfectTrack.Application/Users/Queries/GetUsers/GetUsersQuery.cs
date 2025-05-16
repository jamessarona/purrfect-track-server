// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetUsersQuery
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Users.Queries.GetUsers;

public record GetUsersQuery(PaginationRequest PaginationRequest)
    : IQuery<GetUsersResult>;

public record GetUsersResult(PaginatedResult<UserModel> Users);