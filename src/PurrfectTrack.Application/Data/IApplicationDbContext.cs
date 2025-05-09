using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

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

    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}