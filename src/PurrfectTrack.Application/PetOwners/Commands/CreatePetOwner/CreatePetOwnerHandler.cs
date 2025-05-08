using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.Exceptions;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Infrastructure.Caching;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.PetOwners.Commands.CreatePetOwner;

public class CreatePetOwnerHandler
    : BaseHandler, ICommandHandler<CreatePetOwnerCommand, CreatePetOwnerResult>
{
    private readonly ICacheService _cacheService;

    public CreatePetOwnerHandler(IApplicationDbContext dbContext, ICacheService cacheService)
        : base(dbContext)
    {
        _cacheService = cacheService;
    }

    public async Task<CreatePetOwnerResult> Handle(CreatePetOwnerCommand command, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);

        if (user == null)
            throw new UserNotFoundException(command.UserId);

        var petOwner = new PetOwner(
            command.UserId,
            command.FirstName,
            command.LastName,
            command.PhoneNumber,
            command.Address,
            command.DateOfBirth,
            command.Gender);

        dbContext.PetOwners.Add(petOwner);

        await dbContext.SaveChangesAsync(cancellationToken);

        await _cacheService.RemoveAsync(CacheKeyManager.GetPetOwnersCacheKey());

        return new CreatePetOwnerResult(petOwner.Id);
    }
}