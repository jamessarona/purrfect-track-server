using Microsoft.EntityFrameworkCore;

namespace PurrfectTrack.Application.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<PetOwner> PetOwners { get; }
    DbSet<Pet> Pets { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}