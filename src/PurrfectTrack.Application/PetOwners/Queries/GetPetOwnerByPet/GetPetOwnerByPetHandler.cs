using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Application.Exceptions;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.PetOwners.Queries.GetPetOwnerByPet;

public class GetPetOwnerByPetHandler
    : BaseQueryHandler, IQueryHandler<GetPetOwnerByPetQuery, GetPetOwnerByPetResult>
{
    public GetPetOwnerByPetHandler(IApplicationDbContext dbContext, IMapper mapper) 
        : base(dbContext, mapper) { }

    public async Task<GetPetOwnerByPetResult> Handle(GetPetOwnerByPetQuery query, CancellationToken cancellationToken)
    {
        var petOwner = await dbContext.PetOwners
            .Where(po => po.Pets.Any(p => p.Id == query.PetId))
            .Include(po => po.User)
            .Include(po => po.Pets)
            .ProjectTo<PetOwnerModel>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (petOwner is null)
            throw new PetOwnerNotFoundException(query.PetId);

        return new GetPetOwnerByPetResult(petOwner);
    }
}
