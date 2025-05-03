using Microsoft.EntityFrameworkCore;

namespace PurrfectTrack.Application.Data;

public interface IApplicationDbContext
{
    DbSet<PetOwner> PetOwners { get; }
    DbSet<Pet> Pets { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}