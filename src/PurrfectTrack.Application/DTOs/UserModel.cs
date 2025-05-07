using PurrfectTrack.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PurrfectTrack.Application.DTOs;

public class UserModel
{
    public Guid Id { get; set; }

    [EmailAddress]
    public string Email { get; set; } = default!;

    public UserRole Role { get; set; }
    public bool IsActive { get; set; } = true;
}