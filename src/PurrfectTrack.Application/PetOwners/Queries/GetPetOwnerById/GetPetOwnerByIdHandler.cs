using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Application.Exceptions;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.PetOwners.Queries.GetPetOwnerById;

public class GetPetOwnerByIdHandler
    : BaseQueryHandler, IQueryHandler<GetPetOwnerByIdQuery, GetPetOwnerByIdResult>
{
    public GetPetOwnerByIdHandler(IApplicationDbContext dbContext, IMapper mapper) 
        : base(dbContext, mapper) { }

    public async Task<GetPetOwnerByIdResult> Handle(GetPetOwnerByIdQuery query, CancellationToken cancellationToken)
    {
        var petOwner = await dbContext.PetOwners
            .Include(p => p.User)
            .Include(p => p.Pets)
            .SingleOrDefaultAsync(p => p.Id == query.Id, cancellationToken);

        if (petOwner is null)
            throw new PetOwnerNotFoundException(query.Id);

        var petOwnerModel = mapper.Map<PetOwnerModel>(petOwner);


        return new GetPetOwnerByIdResult(petOwnerModel);
    }
}
