// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetUsersByRoleHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Users.Queries.GetUsersByRole;

public class GetUsersByRoleHandler : BaseQueryHandler, IQueryHandler<GetUsersByRoleQuery, GetUsersByRoleResult>
{
    public GetUsersByRoleHandler(IApplicationDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper) { }

    public async Task<GetUsersByRoleResult> Handle(GetUsersByRoleQuery query, CancellationToken cancellationToken)
    {
        var role = query.Role;
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var usersCount = await dbContext.Users
            .Where(u => u.Role.ToString() == role)
            .CountAsync(cancellationToken);

        var users = await dbContext.Users
            .Where(u => u.Role.ToString() == role)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var userModels = mapper.Map<List<UserModel>>(users);

        var paginatedResult = new PaginatedResult<UserModel>(pageIndex, pageSize, usersCount, userModels);

        return new GetUsersByRoleResult(paginatedResult);
    }
}
      