// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetVetStaffsByCompanyHandler
//  Created:     5/17/2025 4:52:53 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.VetStaffs.Queries.GetVetStaffsByCompany;

public class GetVetStaffsByCompanyHandler
    : BaseQueryHandler, IQueryHandler<GetVetStaffsByCompanyQuery, GetVetStaffsByCompanyResult>
{
    public GetVetStaffsByCompanyHandler(IApplicationDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper)
    {
    }

    public async Task<GetVetStaffsByCompanyResult> Handle(GetVetStaffsByCompanyQuery query, CancellationToken cancellationToken)
    {
        var vetStaffs = await dbContext.VetStaffs
            .Where(vs => vs.CompanyId == query.CompanyId)
            .ProjectTo<VetStaffModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetVetStaffsByCompanyResult(vetStaffs);
    }
}