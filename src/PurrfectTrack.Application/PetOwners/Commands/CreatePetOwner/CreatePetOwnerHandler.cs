using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.Exceptions;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Domain.Enums;
using PurrfectTrack.Infrastructure.Caching;
using PurrfectTrack.Shared.CQRS;
using PurrfectTrack.Shared.Security;

namespace PurrfectTrack.Application.PetOwners.Commands.CreatePetOwner;

public class CreatePetOwnerHandler
    : BaseHandler, ICommandHandler<CreatePetOwnerCommand, CreatePetOwnerResult>
{
    private readonly ICacheService _cacheService;
    private readonly IPasswordHasher _passwordHasher;

    public CreatePetOwnerHandler(IApplicationDbContext dbContext, ICacheService cacheService, IPasswordHasher passwordHasher)
        : base(dbContext)
    {
        _cacheService = cacheService;
        _passwordHasher = passwordHasher;
    }

    public async Task<CreatePetOwnerResult> Handle(CreatePetOwnerCommand command, CancellationToken cancellationToken)
    {
        var emailExists = await dbContext.Users
            .AnyAsync(u => u.Email == command.Email, cancellationToken);

        if (emailExists)
            throw new InvalidOperationException($"Email '{command.Email}' is already in use.");

        var hashedPassword = _passwordHasher.Hash(command.Password);

        var user = new User(
            command.Email,
            hashedPassword,
            UserRole.PetOwner
        );

        var petOwner = new PetOwner(
            user.Id,
            command.FirstName,
            command.LastName,
            command.PhoneNumber,
            command.Address,
            command.DateOfBirth,
            command.Gender
        );

        var dbTransaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            dbContext.Users.Add(user);
            dbContext.PetOwners.Add(petOwner);

            await dbContext.SaveChangesAsync(cancellationToken);

            await dbTransaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await dbTransaction.RollbackAsync(cancellationToken);
            throw;
        }

        await _cacheService.RemoveAsync(CacheKeyManager.GetPetOwnersCacheKey());

        return new CreatePetOwnerResult(petOwner.Id);
    }
}