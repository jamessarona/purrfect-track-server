using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Abstractions;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Application.Exceptions;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Infrastructure.Caching;
using PurrfectTrack.Shared.CQRS;

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
