// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        RefreshToken
//  Created:     5/16/2025 6:28:24 PM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Domain.Entities;

public class RefreshToken : Entity<Guid> 
{
    public Guid UserId { get; set; }
    public string Token { get; set; } = default!;
    public DateTime? ExpiresAt { get; set; }
    public bool IsRevoked { get; set; } = false;
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public string? ReplacedByToken { get; set; }
    public virtual User User { get; set; } = default!;
}