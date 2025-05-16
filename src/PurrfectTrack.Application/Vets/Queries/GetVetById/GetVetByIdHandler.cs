// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetVetByIdHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Vets.Queries.GetVetById;

public class GetVetByIdHandler :
    BaseQueryHandler, IQueryHandler<GetVetByIdQuery, GetVetByIdResult>
{
    private readonly ICacheService _cacheService;
    public GetVetByIdHandler(IApplicationDbContext dbContext, IMapper mapper, ICacheService cacheService) 
        : base(dbContext, mapper)
    {
        _cacheService = cacheService;
    }

    public async Task<GetVetByIdResult> Handle(GetVetByIdQuery query, CancellationToken cancellationToken)
    {
        var cacheKey = CacheKeyManager.GetVetByIdCacheKey(query.Id);
        var cachedVet = await _cacheService.GetAsync<VetModel>(cacheKey);

        if (cachedVet != null)
            return new GetVetByIdResult(cachedVet);

        var vet = await dbContext.Vets
            .Include(v => v.User)
            .SingleOrDefaultAsync(v => v.Id == query.Id, cancellationToken);

        if (vet == null)
            throw new VetNotFoundException(query.Id);

        var vetModel = mapper.Map<VetModel>(vet);

        await _cacheService.SetAsync(cacheKey, vetModel, TimeSpan.FromDays(1));

        return new GetVetByIdResult(vetModel);
    }
}
