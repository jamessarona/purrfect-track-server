using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Shared.CQRS;
using PurrfectTrack.Shared.Pagination;

namespace PurrfectTrack.Application.Users.Queries.GetUsers;

public record GetUsersQuery(PaginationRequest PaginationRequest)
    : IQuery<GetUsersResult>;

public record GetUsersResult(PaginatedResult<UserModel> Users);