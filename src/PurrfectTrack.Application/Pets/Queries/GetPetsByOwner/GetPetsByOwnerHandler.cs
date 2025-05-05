using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Pets.Queries.GetPetsByOwner;

public class GetPetsByOwnerHandler 
    : BaseQueryHandler, IQueryHandler<GetPetsByOwnerQuery, GetPetsByOwnerResult>
{
    public GetPetsByOwnerHandler(IApplicationDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper) { }

    public async Task<GetPetsByOwnerResult> Handle(GetPetsByOwnerQuery query, CancellationToken cancellationToken)
    {
        var pets = await dbContext.Pets
            .Where(p => p.OwnerId == query.OwnerId)
            .ProjectTo<PetModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetPetsByOwnerResult(pets);
    }
}