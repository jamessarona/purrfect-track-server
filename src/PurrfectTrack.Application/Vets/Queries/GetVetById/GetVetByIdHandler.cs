using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Application.Exceptions;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Infrastructure.Caching;
using PurrfectTrack.Shared.CQRS;

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
