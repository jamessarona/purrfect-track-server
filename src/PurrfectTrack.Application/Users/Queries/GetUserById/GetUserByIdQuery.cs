using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IQuery<GetUserByIdResult>;

public record GetUserByIdResult(UserModel User);