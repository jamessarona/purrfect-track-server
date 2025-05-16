// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        AppointmentModel
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.DTOs;

public class AppointmentModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Location { get; set; }

    public Guid CompanyId { get; set; }
    public CompanyModel? Company { get; set; }

    public Guid PetOwnerId { get; set; }
    public PetOwnerModel? PetOwner { get; set; }

    public Guid? PetId { get; set; }
    public PetModel? Pet { get; set; }

    public Guid? VetId { get; set; }
    public VetModel? Vet { get; set; }

    public Guid? VetStaffId { get; set; }
    public VetStaffModel? VetStaff { get; set; }

    public AppointmentStatus Status { get; set; }
    public string? Notes { get; set; }
}
