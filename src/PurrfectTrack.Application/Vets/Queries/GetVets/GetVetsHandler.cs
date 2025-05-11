using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Infrastructure.Caching;
using PurrfectTrack.Shared.CQRS;

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