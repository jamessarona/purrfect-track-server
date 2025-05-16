// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetPetsByOwnerHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Pets.Queries.GetPetsByOwner;

public class GetPetsByOwnerHandler 
    : BaseQueryHandler, IQueryHandler<GetPetsByOwnerQuery, GetPetsByOwnerResult>
{
    public GetPetsByOwnerHandler(IApplicationDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper) { }

    public async Task<GetPetsByOwnerResult> Handle(GetPetsByOwnerQuery query, CancellationToken cancellationToken)
    {
        var pets = await dbContext.Pets
            .Where(p => p.PetOwnerId == query.OwnerId)
            .ProjectTo<PetModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetPetsByOwnerResult(pets);
    }
}