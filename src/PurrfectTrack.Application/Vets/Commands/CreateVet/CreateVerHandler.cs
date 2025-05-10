using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Abstractions;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Domain.Enums;
using PurrfectTrack.Infrastructure.Caching;
using PurrfectTrack.Shared.CQRS;
using PurrfectTrack.Shared.Security;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PurrfectTrack.Application.Vets.Commands.CreateVet;

public class CreateVerHandler
    : BaseHandler, ICommandHandler<CreateVetCommand, CreateVetResult>
{
    private readonly ICacheService _cacheService;
    private readonly IPasswordHasher _passwordHasher;

    public CreateVerHandler(IApplicationDbContext dbContext, ICacheService cacheService, IPasswordHasher passwordHasher)
        : base(dbContext)
    {
        _cacheService = cacheService;
        _passwordHasher = passwordHasher;
    }

    public async Task<CreateVetResult> Handle(CreateVetCommand command, CancellationToken cancellationToken)
    {
        var emailExists = await dbContext.Users
            .AnyAsync(u => u.Email == command.Email, cancellationToken);

        if (emailExists)
            throw new InvalidOperationException($"Email '{command.Email}' is already in use.");

        var hashedPassword = _passwordHasher.Hash(command.Password);

        var user = new User(
            command.Email,
            hashedPassword,
            UserRole.Vet
        );

        var vet = new Vet(
            user.Id,
            command.FirstName,
            command.LastName,
            command.PhoneNumber,
            command.Address,
            command.DateOfBirth,
            command.Gender,
            command.LicenseNumber,
            command.LicenseExpiryDate,
            command.Specialization,
            command.YearsOfExperience,
            command.ClinicName,
            command.ClinicAddress,
            command.EmploymentDate
        );

        var dbTransaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            dbContext.Users.Add(user);
            dbContext.Vets.Add(vet);

            await dbContext.SaveChangesAsync(cancellationToken);

            await dbTransaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await dbTransaction.RollbackAsync(cancellationToken);
            throw;
        }

        await _cacheService.RemoveAsync(CacheKeyManager.GetVetsCacheKey());

        return new CreateVetResult(vet.Id);
    }
}