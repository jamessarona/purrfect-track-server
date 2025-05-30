﻿// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        VetStaff
//  Created:     5/16/2025 6:28:24 PM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Domain.Entities;

public class VetStaff : Contact
{
    public string? Position { get; set; }
    public string? Department { get; set; }
    public DateTime? EmploymentDate { get; set; }

    public Guid? CompanyId { get; set; }
    public Company? Company { get; set; } = default!;

    protected VetStaff() { }

    public VetStaff(Guid userId, string firstName, string lastName,
        string? phoneNumber, string? address,
        DateTime? dateOfBirth, string? gender,
        string? position, string? department,
        DateTime? employmentDate, Guid? companyId)
        : base(userId, firstName, lastName, phoneNumber, address, dateOfBirth, gender)
    {
        Position = position;
        Department = department;
        EmploymentDate = employmentDate;
        CompanyId = companyId;
    }
}