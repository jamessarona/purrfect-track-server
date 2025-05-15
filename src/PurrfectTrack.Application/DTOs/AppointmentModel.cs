using PurrfectTrack.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PurrfectTrack.Application.DTOs;

public class AppointmentModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Location { get; set; }

    public Guid PetOwnerId { get; set; }
    public PetOwner? PetOwner { get; set; }

    public Guid? PetId { get; set; }
    public Pet? Pet { get; set; }

    public Guid? VetId { get; set; }
    public Vet? Vet { get; set; }

    public Guid? VetStaffId { get; set; }
    public VetStaff? VetStaff { get; set; }

    public AppointmentStatus Status { get; set; }
    public string? Notes { get; set; }
}
