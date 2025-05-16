// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetVetsHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Vets.Queries.GetVets;

public class GetVetsHandler
    : BaseQueryHandler, IQueryHandler<GetVetsQuery, GetVetsResult>
{
    private readonly ICacheService _cacheService;

    public GetVetsHandler(IApplicationDbContext dbContext, IMapper mapper, ICacheService cacheService) 
        : base(dbContext, mapper)
    {
        _cacheService = cacheService;
    }

    public async Task<GetVetsResult> Handle(GetVetsQuery query, CancellationToken cancellationToken)
    {
        var cacheKey = CacheKeyManager.GetVetsCacheKey();
        var cachedVets = await _cacheService.GetAsync<List<VetModel>>(cacheKey);

        if (cachedVets != null)
            return new GetVetsResult(cachedVets);

        var vets = await dbContext.Vets
            .Include(v => v.User)
            .ProjectTo<VetModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        await _cacheService.SetAsync(cacheKey, vets, TimeSpan.FromDays(1));

        return new GetVetsResult(vets);
    }
}