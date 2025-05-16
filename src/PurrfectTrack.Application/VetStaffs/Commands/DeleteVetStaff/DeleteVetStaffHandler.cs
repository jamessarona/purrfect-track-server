// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        DeleteVetStaffHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.VetStaffs.Commands.DeleteVetStaff;

public class DeleteVetStaffHandler
    : BaseHandler, ICommandHandler<DeleteVetStaffCommand, DeleteVetStaffResult>
{
    private readonly ICacheService _cacheService;

    public DeleteVetStaffHandler(IApplicationDbContext dbContext, ICacheService cacheService) 
        : base(dbContext)
    {
        _cacheService = cacheService;
    }

    public async Task<DeleteVetStaffResult> Handle(DeleteVetStaffCommand command, CancellationToken cancellationToken)
    {
        var vetStaff = await dbContext.VetStaffs
            .Include(v => v.User)
            .FirstOrDefaultAsync(v => v.Id == command.Id, cancellationToken);

        if (vetStaff is null)
            return new DeleteVetStaffResult(false);
        
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            dbContext.VetStaffs.Remove(vetStaff);
            dbContext.Users.Remove(vetStaff.User);

            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        await _cacheService.RemoveAsync(CacheKeyManager.GetVetStaffsCacheKey());
        await _cacheService.RemoveAsync(CacheKeyManager.GetVetStaffByIdCacheKey(vetStaff.Id));

        return new DeleteVetStaffResult(true);
    }
}
