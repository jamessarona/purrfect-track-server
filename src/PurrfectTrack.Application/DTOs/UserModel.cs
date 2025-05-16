// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        UserModel
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.DTOs;

public class UserModel
{
    public Guid Id { get; set; }

    [EmailAddress]
    public string Email { get; set; } = default!;

    public UserRole Role { get; set; }
    public bool IsActive { get; set; } = true;
}