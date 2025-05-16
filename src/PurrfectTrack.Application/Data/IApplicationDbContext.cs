// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        IApplicationDbContext
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<UserSession> UserSessions { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<PetOwner> PetOwners { get; }
    DbSet<Pet> Pets { get; }
    DbSet<Vet> Vets { get; }
    DbSet<VetStaff> VetStaffs { get; }
    DbSet<Appointment> Appointments { get; }
    DbSet<Company> Companies { get; }

    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}