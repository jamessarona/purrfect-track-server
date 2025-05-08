using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurrfectTrack.Domain.Entities;

public class UserSession : Entity<Guid>
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string Token { get; set; } = default!;

    public DateTime? ExpiresAt { get; set; }

    public bool IsRevoked { get; set; } = false;

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public virtual User User { get; set; } = default!;
}
