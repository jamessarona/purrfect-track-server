using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Abstractions;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.Exceptions;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Infrastructure.Caching;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.PetOwners.Commands.UpdatePetOwner;

public class UpdatePetOwnerHandler
    : BaseHandler, ICommandHandler<UpdatePetOwnerCommand, UpdatePetOwnerResult>
{
    private readonly ICacheService _cacheService;

    public UpdatePetOwnerHandler(IApplicationDbContext dbContext, ICacheService cacheService)
        : base(dbContext)
    {
        _cacheService = cacheService;
    }

    public async Task<UpdatePetOwnerResult> Handle(UpdatePetOwnerCommand command, CancellationToken cancellationToken)
    {
        var petOwner = await dbContext.PetOwners
            .Include(po => po.Pets)
            .FirstOrDefaultAsync(po => po.Id == command.Id, cancellationToken);

        if (petOwner is null)
            throw new PetOwnerNotFoundException(command.Id);

        petOwner.FirstName = command.FirstName;
        petOwner.LastName = command.LastName;
        petOwner.PhoneNumber = command.PhoneNumber ?? petOwner.PhoneNumber;
        petOwner.Address = command.Address ?? petOwner.Address;
        petOwner.DateOfBirth = command.DateOfBirth ?? petOwner.DateOfBirth;
        petOwner.Gender = command.Gender ?? petOwner.Gender;

        await dbContext.SaveChangesAsync(cancellationToken);

        await _cacheService.RemoveAsync(CacheKeyManager.GetPetOwnersCacheKey());
        await _cacheService.RemoveAsync(CacheKeyManager.GetPetOwnerByIdCacheKey(petOwner.Id));
        foreach (var pet in petOwner.Pets)
            await _cacheService.RemoveAsync(CacheKeyManager.GetPetOwnerByPetCacheKey(pet.Id));

        return new UpdatePetOwnerResult(petOwner.Id);
    }
}