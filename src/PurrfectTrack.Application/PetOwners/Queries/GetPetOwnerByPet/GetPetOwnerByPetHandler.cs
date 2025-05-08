using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Abstractions;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Application.Exceptions;
using PurrfectTrack.Application.PetOwners.Queries.GetPetOwners;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Domain.Entities;
using PurrfectTrack.Infrastructure.Caching;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.PetOwners.Queries.GetPetOwnerByPet;

public class GetPetOwnerByPetHandler
    : BaseQueryHandler, IQueryHandler<GetPetOwnerByPetQuery, GetPetOwnerByPetResult>
{
    private readonly ICacheService _cacheService;

    public GetPetOwnerByPetHandler(IApplicationDbContext dbContext, IMapper mapper, ICacheService cacheService) 
        : base(dbContext, mapper)
    {
        _cacheService = cacheService;
    }

    public async Task<GetPetOwnerByPetResult> Handle(GetPetOwnerByPetQuery query, CancellationToken cancellationToken)
    {
        var cacheKey = CacheKeyManager.GetPetOwnerByPetCacheKey(query.PetId);
        var cachedPetOwner = await _cacheService.GetAsync<PetOwnerModel>(cacheKey);

        if (cachedPetOwner != null)
            return new GetPetOwnerByPetResult(cachedPetOwner);

        var petOwner = await dbContext.PetOwners
            .Where(po => po.Pets.Any(p => p.Id == query.PetId))
            .Include(po => po.User)
            .Include(po => po.Pets)
            .ProjectTo<PetOwnerModel>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (petOwner is null)
            throw new PetOwnerNotFoundException(query.PetId);

        await _cacheService.SetAsync(cacheKey, petOwner, TimeSpan.FromDays(1));

        return new GetPetOwnerByPetResult(petOwner);
    }
}
