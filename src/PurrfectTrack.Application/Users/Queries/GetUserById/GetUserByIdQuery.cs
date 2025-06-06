﻿// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetUserByIdQuery
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IQuery<GetUserByIdResult>;

public record GetUserByIdResult(UserModel User);