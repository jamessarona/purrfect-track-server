// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        Appointment
//  Created:     5/16/2025 6:28:24 PM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

using PurrfectTrack.Domain.Enums;

namespace PurrfectTrack.Domain.Entities;

public class Appointment : Entity<Guid>
{
    [Required]
    public string Title { get; set; } = default!;
    public string? Description { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }
    
    public string? Location { get; set; }
    
    [Required]
    public Guid PetOwnerId { get; set; }

    public PetOwner? PetOwner { get; set; }
    
    public Guid? PetId { get; set; }

    public Pet? Pet { get; set; }

    public Guid? VetId { get; set; }

    public Vet? Vet { get; set; }

    public Guid? VetStaffId { get; set; }

    public VetStaff? VetStaff { get; set; }

    [Required]
    public AppointmentStatus? Status { get; set; }
    
    public string? Notes { get; set; }

    public Guid? CompanyId { get; set; }
    public Company? Company { get; set; } = default!;

    protected Appointment() { }

    public Appointment(string title, string? description, DateTime startDate, DateTime endDate, string? location, 
        Guid petOwnerId, Guid? petId, Guid? vetId, Guid? vetStaffId, AppointmentStatus status, string notes, Guid companyId)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Location = location;
        PetOwnerId = petOwnerId;
        PetId = petId;
        VetId = vetId;
        VetStaffId = vetStaffId;
        CompanyId = companyId;
        Status = status;
        Notes = notes;
    }
}