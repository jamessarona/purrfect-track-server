// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        DeletePetOwnerHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.PetOwners.Commands.DeletePetOwner;

public class DeletePetOwnerHandler
    : BaseHandler, ICommandHandler<DeletePetOwnerCommand, DeletePetOwnerResult>
{
    private readonly ICacheService _cacheService;

    public DeletePetOwnerHandler(IApplicationDbContext dbContext, ICacheService cacheService)
        : base(dbContext)
    {
        _cacheService = cacheService;
    }

    public async Task<DeletePetOwnerResult> Handle(DeletePetOwnerCommand command, CancellationToken cancellationToken)
    {
        var petOwner = await dbContext.PetOwners
            .Include(po => po.User)
            .Include(po => po.Pets)
            .FirstOrDefaultAsync(po => po.Id == command.Id, cancellationToken);

        if (petOwner is null)
            return new DeletePetOwnerResult(false);

        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            if (petOwner.Pets is not null)
            {
                foreach (var pet in petOwner.Pets)
                    dbContext.Pets.Remove(pet);
            }
                  
            dbContext.PetOwners.Remove(petOwner);
            dbContext.Users.Remove(petOwner.User);

            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw; 
        }

        await _cacheService.RemoveAsync(CacheKeyManager.GetPetOwnersCacheKey());
        await _cacheService.RemoveAsync(CacheKeyManager.GetPetOwnerByIdCacheKey(petOwner.Id));
        if (petOwner.Pets is not null)
        {
            foreach (var pet in petOwner.Pets)
                await _cacheService.RemoveAsync(CacheKeyManager.GetPetOwnerByPetCacheKey(pet.Id));
            
        }

        return new DeletePetOwnerResult(true);
    }
}