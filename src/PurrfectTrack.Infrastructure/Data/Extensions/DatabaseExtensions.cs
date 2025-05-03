using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PurrfectTrack.Infrastructure.Data.Extensions;


public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context);
    }

    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedPetOwnerAsync(context);
    }

    private static async Task SeedPetOwnerAsync(ApplicationDbContext context)
    {
        if (!await context.PetOwners.AnyAsync())
        {
            //To be implemented
            //await context.PetOwners.AddRangeAsync(InitialData.PetOwners);
            //await context.SaveChangesAsync();
        }
    }
}
