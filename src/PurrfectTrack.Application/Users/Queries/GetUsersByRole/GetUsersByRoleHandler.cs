using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;
using PurrfectTrack.Shared.Pagination;

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
      