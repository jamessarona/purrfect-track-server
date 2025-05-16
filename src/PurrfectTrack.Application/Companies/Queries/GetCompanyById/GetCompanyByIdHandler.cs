// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetCompanyByIdHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Companies.Queries.GetCompanyById;

public class GetCompanyByIdHandler
    : BaseQueryHandler, IQueryHandler<GetCompanyByIdQuery, GetCompanyByIdResult>
{
    public GetCompanyByIdHandler(IApplicationDbContext dbContext, IMapper mapper) 
        : base(dbContext, mapper)
    {
    }

    public async Task<GetCompanyByIdResult> Handle(GetCompanyByIdQuery query, CancellationToken cancellationToken)
    {
        var company = await dbContext.Companies
            .SingleOrDefaultAsync(c => c.Id == query.Id, cancellationToken);

        if (company is null)
            throw new CompanyNotFoundException(query.Id);

        var companyModel = mapper.Map<CompanyModel>(company);

        return new GetCompanyByIdResult(companyModel);
    }
}