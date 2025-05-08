using PurrfectTrack.Domain.Enums;

namespace PurrfectTrack.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

        await context.Database.MigrateAsync();

        await SeedAsync(context, passwordHasher);
    }

    private static async Task SeedAsync(ApplicationDbContext context, IPasswordHasher passwordHasher)
    {
        await SeedUserAndPetOwnerAsync(context, passwordHasher);
    }

    private static async Task SeedUserAndPetOwnerAsync(ApplicationDbContext context, IPasswordHasher passwordHasher)
    {
        if (!await context.Users.AnyAsync())
        {
            var user = new User(
                "james@gmail.com",
                passwordHasher.Hash("M@st3rk3y"),
                UserRole.Administrator
            );

            var petOwner = new PetOwner(
                user.Id,
                "James",
                "Sarona",
                "09397220078",
                "Davao, Philippines",
                new DateTime(1998, 09, 27),
                "Male"
            );

            petOwner.Pets.AddRange(new[]
            {
                new Pet(petOwner.Id, "Nano", "Cat", "Himalayan", "Male", new DateTime(2021, 07, 08), 5.0f, "Cream", true),
                new Pet(petOwner.Id, "Nana", "Cat", "Mainecoon", "Female", new DateTime(2022, 09, 23), 5.0f, "Cream", true),
                new Pet(petOwner.Id, "Nene", "Cat", "Himalayan", "Female", new DateTime(2022, 12, 16), 5.0f, "Cream", true)
            });

            await context.Users.AddAsync(user);
            await context.PetOwners.AddAsync(petOwner);

            await context.SaveChangesAsync();
        }
    }
}
