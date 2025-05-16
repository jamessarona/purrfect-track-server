// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetVetsByCompanyHandler
//  Created:     5/17/2025 4:43:04 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Vets.Queries.GetVetsByCompany;

public class GetVetsByCompanyHandler
    : BaseQueryHandler, IQueryHandler<GetVetsByCompanyQuery, GetVetsByCompanyResult>
{
    public GetVetsByCompanyHandler(IApplicationDbContext dbContext, IMapper mapper) 
        : base(dbContext, mapper)
    {
    }

    public async Task<GetVetsByCompanyResult> Handle(GetVetsByCompanyQuery query, CancellationToken cancellationToken)
    {
        var vets = await dbContext.Vets
            .Where(v => v.CompanyId == query.CompanyId)
            .ProjectTo<VetModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetVetsByCompanyResult(vets);
    }
}