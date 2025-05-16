// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        Vet
//  Created:     5/16/2025 6:28:24 PM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Domain.Entities;

public class Vet : Contact
{
    public string? LicenseNumber { get; set; }
    public DateTime? LicenseExpiryDate { get; set; }
    public string? Specialization { get; set; }
    public int? YearsOfExperience { get; set; }
    public string? ClinicName { get; set; }
    public string? ClinicAddress { get; set; }
    public DateTime? EmploymentDate { get; set; }

    public Guid? CompanyId { get; set; }
    public Company? Company { get; set; } = default!;

    protected Vet() { }

    public Vet(Guid userId, string firstName, string lastName,
        string? phoneNumber = null, string? address = null,
        DateTime? dateOfBirth = null, string? gender = null,
        string? licenseNumber = null, DateTime? licenseExpiryDate = null,
        string? specialization = null, int? yearsOfExperience = null,
        string? clinicName = null, string? clinicAddress = null,
        DateTime? employmentDate = null, Guid? companyId = null)
        : base(userId, firstName, lastName, phoneNumber, address, dateOfBirth, gender)
    {
        LicenseNumber = licenseNumber;
        LicenseExpiryDate = licenseExpiryDate;
        Specialization = specialization;
        YearsOfExperience = yearsOfExperience;
        ClinicName = clinicName;
        ClinicAddress = clinicAddress;
        EmploymentDate = employmentDate;
        CompanyId = companyId;
    }
}