// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetUsersHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------


namespace PurrfectTrack.Application.Users.Queries.GetUsers;

public class GetUsersHandler : BaseQueryHandler, IQueryHandler<GetUsersQuery, GetUsersResult>
{
    public GetUsersHandler(IApplicationDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper) { }

    public async Task<GetUsersResult> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize; 
        
        var userQuery = dbContext.Users.AsQueryable();

        var totalCount = await userQuery.CountAsync(cancellationToken);

        var users = await userQuery
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var userModels = mapper.Map<List<UserModel>>(users);

        var paginatedResult = new PaginatedResult<UserModel>(
            pageIndex,
            pageSize,
            totalCount,
            userModels
        );

        return new GetUsersResult(paginatedResult);
    }
}