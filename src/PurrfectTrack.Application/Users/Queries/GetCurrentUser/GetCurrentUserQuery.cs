// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetCurrentUserQuery
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Users.Queries.GetCurrentUser;

public record GetCurrentUserQuery : IQuery<GetCurrentUserResult>;

public record GetCurrentUserResult(UserModel User);