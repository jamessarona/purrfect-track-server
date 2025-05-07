using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Application.Exceptions;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Pets.Queries.GetPetById;

public class GetPetByIdHandler
    : BaseQueryHandler, IQueryHandler<GetPetByIdQuery, GetPetByIdResult>
{
    public GetPetByIdHandler(IApplicationDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper) { }

    public async Task<GetPetByIdResult> Handle(GetPetByIdQuery query, CancellationToken cancellationToken)
    {
        var pet = await dbContext.Pets
            .FirstOrDefaultAsync(p => p.Id == query.PetId, cancellationToken);

        if (pet is null)
            throw new PetNotFoundException(query.PetId);

        var petModel = mapper.Map<PetModel>(pet);

        return new GetPetByIdResult(petModel);
    }
}
