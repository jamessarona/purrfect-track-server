// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetPetOwnersHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.PetOwners.Queries.GetPetOwners;

public class GetPetOwnersHandler
    : BaseQueryHandler, IQueryHandler<GetPetOwnersQuery, GetPetOwnersResult>
{
    private readonly ICacheService _cacheService;

    public GetPetOwnersHandler(IApplicationDbContext dbContext, IMapper mapper, ICacheService cacheService) 
        : base(dbContext, mapper) 
    {
        _cacheService = cacheService;
    }

    public async Task<GetPetOwnersResult> Handle(GetPetOwnersQuery query, CancellationToken cancellationToken)
    {
        var cacheKey = CacheKeyManager.GetPetOwnersCacheKey();
        var cachedPetOwners = await _cacheService.GetAsync<List<PetOwnerModel>>(cacheKey);

        if (cachedPetOwners != null)
            return new GetPetOwnersResult(cachedPetOwners);

        var petOwners = await dbContext.PetOwners 
            .Include(po => po.User)
            .Include(p => p.Pets)
            .ProjectTo<PetOwnerModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        await _cacheService.SetAsync(cacheKey, petOwners, TimeSpan.FromDays(1));

        return new GetPetOwnersResult(petOwners);
    }
}