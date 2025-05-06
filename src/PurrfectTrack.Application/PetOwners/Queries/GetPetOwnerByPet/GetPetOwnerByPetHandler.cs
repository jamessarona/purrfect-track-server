using AutoMapper;
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
            .Select(po => new
            {
                PetOwner = po,
                Pets = po.Pets.Where(p => p.Id == query.PetId).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (petOwner is null)
            throw new PetOwnerNotFoundException(query.PetId);

        var petOwnerModel = mapper.Map<PetOwnerModel>(petOwner.PetOwner);

        petOwnerModel.Pets = mapper.Map<List<PetModel>>(petOwner.Pets);

        return new GetPetOwnerByPetResult(petOwnerModel);
    }
}
