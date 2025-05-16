// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        UserSession
//  Created:     5/16/2025 6:28:24 PM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurrfectTrack.Domain.Entities;

public class UserSession : Entity<Guid>
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public string Token { get; set; } = default!;
    public DateTime? ExpiresAt { get; set; }
    public bool IsRevoked { get; set; } = false;
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public virtual User User { get; set; } = default!;
}
