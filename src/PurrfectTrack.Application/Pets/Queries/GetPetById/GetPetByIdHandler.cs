// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetPetByIdHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

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
