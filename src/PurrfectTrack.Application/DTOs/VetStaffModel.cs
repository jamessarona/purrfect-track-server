// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        VetStaffModel
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.DTOs;

public class VetStaffModel : ContactModel
{
    public string? Position { get; set; }
    public string? Department { get; set; }
    public DateTime? EmploymentDate { get; set; }
    public Guid? CompanyId { get; set; }

    public CompanyModel? Company { get; set; }
}