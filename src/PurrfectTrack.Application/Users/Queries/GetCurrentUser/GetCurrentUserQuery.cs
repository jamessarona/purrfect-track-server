using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Users.Queries.GetCurrentUser;

public record GetCurrentUserQuery : IQuery<GetCurrentUserResult>;

public record GetCurrentUserResult(UserModel User);