using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Pets.Queries.GetPets;

public class GetPetsHandler
    : BaseQueryHandler, IQueryHandler<GetPetsQuery, GetPetsResult>
{
    public GetPetsHandler(IApplicationDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper) { }

    public async Task<GetPetsResult> Handle(GetPetsQuery query, CancellationToken cancellationToken)
    {
        var pets = await dbContext.Pets
            .ProjectTo<PetModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetPetsResult(pets);
    }
}