// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        User
//  Created:     5/16/2025 6:28:24 PM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Domain.Entities;

public class User : Entity<Guid>
{
    [Required]
    public string Email { get; set; } = default!;
    [Required]
    public string PasswordHash { get; set; } = default!;

    public UserRole Role { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime? LastLoginAt { get; set; }
    public int FailedLoginCount { get; set; } = 0;
    public DateTime? LockoutEnd { get; set; }

    public Vet? VetProfile { get; set; }
    public VetStaff? VetStaffProfile { get; set; }
    public PetOwner? PetOwnerProfile { get; set; }

    public User(string email, string passwordHash, UserRole role)
    {
        Id = Guid.NewGuid();
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }
}
