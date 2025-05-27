// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        ICurrentUserService
//  Created:     5/27/2025 3:17:45 PM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Abstractions;

public interface ICurrentUserService
{
    Guid UserId { get; }
}