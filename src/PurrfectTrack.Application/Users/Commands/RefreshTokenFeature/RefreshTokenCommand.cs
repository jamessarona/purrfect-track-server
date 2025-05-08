using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Users.Commands.RefreshTokenFeature;

public record RefreshTokenCommand(string RefreshToken) : ICommand<RefreshTokenResult>;

public record RefreshTokenResult(string AccessToken, string RefreshToken);