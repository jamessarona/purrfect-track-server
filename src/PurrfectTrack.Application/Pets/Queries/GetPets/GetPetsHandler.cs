// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetPetsHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

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