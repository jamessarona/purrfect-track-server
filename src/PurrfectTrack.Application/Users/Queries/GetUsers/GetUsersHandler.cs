using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;
using PurrfectTrack.Shared.Pagination;


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