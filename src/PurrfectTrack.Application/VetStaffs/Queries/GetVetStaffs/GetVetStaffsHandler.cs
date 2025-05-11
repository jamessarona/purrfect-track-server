using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Infrastructure.Caching;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.VetStaffs.Queries.GetVets;

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