// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        DatabaseExtensions
//  Created:     5/17/2025 12:54:12 AM
//
//  This file is part of the PurrfectTack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

using PurrfectTrack.Domain.Entities;

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
        await SeedAdminAsync(context, passwordHasher);
        await SeedUserAndPetOwnerAsync(context, passwordHasher);
        await SeedCompanyAndStaffAsync(context, passwordHasher);
    }

    private static async Task SeedAdminAsync(ApplicationDbContext context, IPasswordHasher passwordHasher)
    {
        if (!await context.Users.AnyAsync())
        {
            var user = new User(
                "james@gmail.com",
                passwordHasher.Hash("M@st3rk3y"),
                UserRole.Administrator
            );

            await context.Users.AddAsync(user);

            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedUserAndPetOwnerAsync(ApplicationDbContext context, IPasswordHasher passwordHasher)
    {
        if (!await context.PetOwners.AnyAsync())
        {
            var user = new User(
                "petowner@gmail.com",
                passwordHasher.Hash("petownerpass"),
                UserRole.PetOwner
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

    private static async Task SeedCompanyAndStaffAsync(ApplicationDbContext context, IPasswordHasher passwordHasher)
    {
        if (await context.Companies.AnyAsync())
            return;

        var company1 = new Company(
            name: "Happy Pets Clinic",
            description: "Leading veterinary clinic in downtown",
            phoneNumber: "123-456-7890",
            email: "contact@happypets.com",
            website: "https://happypets.com",
            address: "123 Main St, Cityville",
            taxpayerId: "TXP123456",
            isActive: true);

        var company2 = new Company(
            name: "Care & Cure Vet Center",
            description: "Compassionate veterinary care for all animals",
            phoneNumber: "098-765-4321",
            email: "info@careandcure.com",
            website: "https://careandcure.com",
            address: "456 Oak Ave, Townsville",
            taxpayerId: "TXP654321",
            isActive: true);

        await context.Companies.AddRangeAsync(company1, company2);
        await context.SaveChangesAsync();


        var vetUser1 = new User(
            email: "vet@gmail.com",
            passwordHash: passwordHasher.Hash("vetpass"),
            role: UserRole.Vet);

        var vetUser2 = new User(
            email: "vet2@happypets.com",
            passwordHash: passwordHasher.Hash("VetPass2!"),
            role: UserRole.Vet);

        var vetUser3 = new User(
            email: "vet3@careandcure.com",
            passwordHash: passwordHasher.Hash("VetPass3!"),
            role: UserRole.Vet);

        await context.Users.AddRangeAsync(vetUser1, vetUser2, vetUser3);
        await context.SaveChangesAsync();

        var vet1 = new Vet(
            userId: vetUser1.Id,
            firstName: "Alice",
            lastName: "Smith",
            phoneNumber: "555-0101",
            address: "123 Main St, Cityville",
            dateOfBirth: new DateTime(1980, 5, 15),
            gender: "Female",
            licenseNumber: "LIC123456",
            licenseExpiryDate: DateTime.Today.AddYears(2),
            specialization: "Surgery",
            yearsOfExperience: 10,
            clinicName: "Happy Pets Clinic",
            clinicAddress: company1.Address,
            employmentDate: new DateTime(2015, 6, 1),
            companyId: company1.Id);

        var vet2 = new Vet(
            userId: vetUser2.Id,
            firstName: "Bob",
            lastName: "Johnson",
            phoneNumber: "555-0102",
            address: "124 Main St, Cityville",
            dateOfBirth: new DateTime(1978, 9, 20),
            gender: "Male",
            licenseNumber: "LIC654321",
            licenseExpiryDate: DateTime.Today.AddYears(1),
            specialization: "Dermatology",
            yearsOfExperience: 12,
            clinicName: "Happy Pets Clinic",
            clinicAddress: company1.Address,
            employmentDate: new DateTime(2014, 3, 15),
            companyId: company1.Id);

        var vet3 = new Vet(
            userId: vetUser3.Id,
            firstName: "Carol",
            lastName: "Williams",
            phoneNumber: "555-0201",
            address: "456 Oak Ave, Townsville",
            dateOfBirth: new DateTime(1985, 12, 10),
            gender: "Female",
            licenseNumber: "LIC789012",
            licenseExpiryDate: DateTime.Today.AddYears(3),
            specialization: "General Practice",
            yearsOfExperience: 8,
            clinicName: "Care & Cure Vet Center",
            clinicAddress: company2.Address,
            employmentDate: new DateTime(2018, 1, 20),
            companyId: company2.Id);

        await context.Vets.AddRangeAsync(vet1, vet2, vet3);
        await context.SaveChangesAsync();


        // Create VetStaff user and profile
        var vetStaffUser = new User(
            email: "vetstaff@gmail.com",
            passwordHash: passwordHasher.Hash("vetstaffpass"),
            role: UserRole.VetStaff);

        await context.Users.AddAsync(vetStaffUser);
        await context.SaveChangesAsync();

        var vetStaff1 = new VetStaff(
            userId: vetStaffUser.Id,
            firstName: "David",
            lastName: "Miller",
            phoneNumber: "555-0303",
            address: "123 Main St, Cityville",
            dateOfBirth: new DateTime(1990, 7, 7),
            gender: "Male",
            position: "Receptionist",
            department: "Front Desk",
            employmentDate: new DateTime(2020, 5, 1),
            companyId: company1.Id);

        await context.VetStaffs.AddAsync(vetStaff1);
        await context.SaveChangesAsync();
    }

}
