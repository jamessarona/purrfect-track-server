using PurrfectTrack.Domain.Enums;

namespace PurrfectTrack.Application.Abstractions;

public interface IJwtService
{
    string GenerateToken(Guid userId, UserRole role);
    bool ValidateToken(string token);
    string GetUserIdFromToken(string token);
}