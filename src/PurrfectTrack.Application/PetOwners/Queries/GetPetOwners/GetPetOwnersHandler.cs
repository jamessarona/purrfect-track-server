using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.PetOwners.Queries.GetPetOwners;

public class GetPetOwnersHandler
    : BaseQueryHandler, IQueryHandler<GetPetOwnersQuery, GetPetOwnersResult>
{
    public GetPetOwnersHandler(IApplicationDbContext dbContext, IMapper mapper) 
        : base(dbContext, mapper) { }

    public async Task<GetPetOwnersResult> Handle(GetPetOwnersQuery query, CancellationToken cancellationToken)
    {
        var petOwners = await dbContext.PetOwners 
                .Include(po => po.User)
            .Include(p => p.Pets)
            .ProjectTo<PetOwnerModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetPetOwnersResult(petOwners);
    }
}