// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        IJwtService
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Abstractions;

public interface IJwtService
{
    string GenerateToken(Guid userId, UserRole role);
    bool ValidateToken(string token);
    string GetUserIdFromToken(string token);
}