// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetPetOwnerByIdHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.PetOwners.Queries.GetPetOwnerById;

public class GetPetOwnerByIdHandler
    : BaseQueryHandler, IQueryHandler<GetPetOwnerByIdQuery, GetPetOwnerByIdResult>
{
    private readonly ICacheService _cacheService;

    public GetPetOwnerByIdHandler(IApplicationDbContext dbContext, IMapper mapper, ICacheService cacheService) 
        : base(dbContext, mapper)
    {
        _cacheService = cacheService;
    }

    public async Task<GetPetOwnerByIdResult> Handle(GetPetOwnerByIdQuery query, CancellationToken cancellationToken)
    {
        var cacheKey = CacheKeyManager.GetPetOwnerByIdCacheKey(query.Id);
        var cachedPetOwner = await _cacheService.GetAsync<PetOwnerModel>(cacheKey);

        if (cachedPetOwner != null)
            return new GetPetOwnerByIdResult(cachedPetOwner);

        var petOwner = await dbContext.PetOwners
            .Include(p => p.User)
            .Include(p => p.Pets)
            .SingleOrDefaultAsync(p => p.Id == query.Id, cancellationToken);

        if (petOwner is null)
            throw new PetOwnerNotFoundException(query.Id);

        var petOwnerModel = mapper.Map<PetOwnerModel>(petOwner);

        if (petOwner != null)
            await _cacheService.SetAsync(cacheKey, petOwnerModel, TimeSpan.FromDays(1));

        return new GetPetOwnerByIdResult(petOwnerModel);
    }
}
