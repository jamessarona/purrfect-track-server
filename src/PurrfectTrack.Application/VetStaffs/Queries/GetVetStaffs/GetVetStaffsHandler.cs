// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetVetStaffsHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.VetStaffs.Queries.GetVetStaffs;

public class GetVetStaffsHandler
    : BaseQueryHandler, IQueryHandler<GetVetStaffsQuery, GetVetStaffsResult>
{
    private readonly ICacheService _cacheService;

    public GetVetStaffsHandler(IApplicationDbContext dbContext, IMapper mapper, ICacheService cacheService) 
        : base(dbContext, mapper)
    {
        _cacheService = cacheService;
    }

    public async Task<GetVetStaffsResult> Handle(GetVetStaffsQuery query, CancellationToken cancellationToken)
    {
        var cacheKey = CacheKeyManager.GetVetStaffsCacheKey();
        var cachedVetStaffs = await _cacheService.GetAsync<List<VetStaffModel>>(cacheKey);

        if (cachedVetStaffs != null)
            return new GetVetStaffsResult(cachedVetStaffs);

        var vetStaffs = await dbContext.VetStaffs
            .Include(v => v.User)
            .ProjectTo<VetStaffModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        await _cacheService.SetAsync(cacheKey, vetStaffs, TimeSpan.FromDays(1));

        return new GetVetStaffsResult(vetStaffs);
    }
}