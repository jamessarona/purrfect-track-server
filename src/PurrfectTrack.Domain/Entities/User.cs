using PurrfectTrack.Domain.Enums;

namespace PurrfectTrack.Domain.Entities;

public class User : Entity<Guid>
{
    [Required]
    public string Email { get; set; } = default!;
    [Required]
    public string PasswordHash { get; set; } = default!;

    public UserRole Role { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime? LastLoginAt { get; set; }
    public int FailedLoginCount { get; set; } = 0;
    public DateTime? LockoutEnd { get; set; }

    public PetOwner? PetOwnerProfile { get; set; }
}
