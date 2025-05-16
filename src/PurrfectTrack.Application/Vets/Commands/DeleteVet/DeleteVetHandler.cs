// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        DeleteVetHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Vets.Commands.DeleteVet;

public class DeleteVetHandler
    : BaseHandler, ICommandHandler<DeleteVetCommand, DeleteVetResult>
{
    private readonly ICacheService _cacheService;

    public DeleteVetHandler(IApplicationDbContext dbContext, ICacheService cacheService) 
        : base(dbContext)
    {
        _cacheService = cacheService;
    }

    public async Task<DeleteVetResult> Handle(DeleteVetCommand command, CancellationToken cancellationToken)
    {
        var vet = await dbContext.Vets
            .Include(v => v.User)
            .FirstOrDefaultAsync(v => v.Id == command.Id, cancellationToken);

        if (vet is null)
            return new DeleteVetResult(false);
        
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            dbContext.Vets.Remove(vet);
            dbContext.Users.Remove(vet.User);

            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        await _cacheService.RemoveAsync(CacheKeyManager.GetVetsCacheKey());
        await _cacheService.RemoveAsync(CacheKeyManager.GetVetByIdCacheKey(vet.Id));

        return new DeleteVetResult(true);
    }
}
