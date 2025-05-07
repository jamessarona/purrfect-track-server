using PurrfectTrack.Domain.Enums;

namespace PurrfectTrack.Application.DTOs;

public class UserModel
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public UserRole Role { get; set; }
    public bool IsActive { get; set; } = true;
}
