// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetVetStaffByIdHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.VetStaffs.Queries.GetVetStaffById;

public class GetVetStaffByIdHandler :
    BaseQueryHandler, IQueryHandler<GetVetStaffByIdQuery, GetVetStaffByIdResult>
{
    private readonly ICacheService _cacheService;
    public GetVetStaffByIdHandler(IApplicationDbContext dbContext, IMapper mapper, ICacheService cacheService) 
        : base(dbContext, mapper)
    {
        _cacheService = cacheService;
    }

    public async Task<GetVetStaffByIdResult> Handle(GetVetStaffByIdQuery query, CancellationToken cancellationToken)
    {
        var cacheKey = CacheKeyManager.GetVetStaffByIdCacheKey(query.Id);
        var cachedVetStaff = await _cacheService.GetAsync<VetStaffModel>(cacheKey);

        if (cachedVetStaff != null)
            return new GetVetStaffByIdResult(cachedVetStaff);

        var vetStaff = await dbContext.VetStaffs
            .Include(v => v.User)
            .SingleOrDefaultAsync(v => v.Id == query.Id, cancellationToken);

        if (vetStaff == null)
            throw new VetStaffNotFoundException(query.Id);

        var vetStaffModel = mapper.Map<VetStaffModel>(vetStaff);

        await _cacheService.SetAsync(cacheKey, vetStaffModel, TimeSpan.FromDays(1));

        return new GetVetStaffByIdResult(vetStaffModel);
    }
}
