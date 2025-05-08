using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Infrastructure.Caching;
using PurrfectTrack.Shared.CQRS;

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