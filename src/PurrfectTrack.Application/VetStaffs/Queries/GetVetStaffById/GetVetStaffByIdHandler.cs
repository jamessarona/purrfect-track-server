using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Application.Exceptions;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Infrastructure.Caching;
using PurrfectTrack.Shared.CQRS;

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
