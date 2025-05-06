using System.Net;

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
            var owner1 = new PetOwner(
                "Alice",
                "Johnson",
                "alice.johnson@example.com",
                "123-456-7890",
                "123 Maple St, Springfield",
                new DateTime(1985, 6, 15),
                "Female"
            );

            var owner2 = new PetOwner(
                "Bob",
                "Smith",
                "bob.smith@example.com",
                "987-654-3210",
                "456 Oak Ave, Metropolis",
                new DateTime(1990, 3, 22),
                "Male"
            )
            {
                Id = Guid.NewGuid()
            };

            var owner3 = new PetOwner(
                "Cathy",
                "Wright",
                "cathy.wright@example.com",
                "555-678-9012",
                "789 Pine Rd, Riverdale",
                new DateTime(1978, 11, 3),
                "Female"
            )
            {
                Id = Guid.NewGuid()
            };

            var owner4 = new PetOwner(
                "David",
                "Lee",
                "david.lee@example.com",
                "222-333-4444",
                "321 Birch Ln, Hilltown",
                new DateTime(2000, 1, 9),
                "Male"
            )
            {
                Id = Guid.NewGuid()
            };

            owner1.Pets.AddRange(new[]
            {
                new Pet(owner1.Id, "Buddy", "Dog", "Golden Retriever", "Male", new DateTime(2020, 5, 1), 30.5f, "Golden", true),
                new Pet(owner1.Id, "Whiskers", "Cat", "Siamese", "Female", new DateTime(2019, 8, 10), 8.2f, "Cream", false)
            });

            owner3.Pets.AddRange(new[]
            {
                new Pet(owner3.Id, "Max", "Dog", "Labrador", "Male", new DateTime(2018, 4, 10), 28.1f, "Black", true),
                new Pet(owner3.Id, "Milo", "Cat", "Maine Coon", "Male", new DateTime(2021, 2, 20), 7.3f, "Gray", false),
                new Pet(owner3.Id, "Luna", "Cat", "Bengal", "Female", new DateTime(2020, 7, 30), 6.8f, "Spotted", true),
                new Pet(owner3.Id, "Rocky", "Dog", "Boxer", "Male", new DateTime(2019, 9, 5), 32.0f, "Brindle", false),
                new Pet(owner3.Id, "Coco", "Rabbit", "Holland Lop", "Female", new DateTime(2022, 1, 15), 2.1f, "White", false)
            });

            owner4.Pets.Add(
                new Pet(owner4.Id, "Bella", "Dog", "Poodle", "Female", new DateTime(2023, 3, 12), 5.5f, "White", false)
            );

            await context.PetOwners.AddRangeAsync(owner1, owner2, owner3, owner4);
            await context.SaveChangesAsync();
        }
    }
}
