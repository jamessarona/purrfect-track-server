using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Shared.CQRS;
using PurrfectTrack.Shared.Pagination;

namespace PurrfectTrack.Application.Users.Queries.GetUsersByRole;

public record GetUsersByRoleQuery(string Role, PaginationRequest PaginationRequest)
    : IQuery<GetUsersByRoleResult>;

public record GetUsersByRoleResult(PaginatedResult<UserModel> Users);