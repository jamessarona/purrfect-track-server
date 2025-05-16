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
    public Company? Company { get; set; }

    protected Vet() { }

    public Vet(Guid userId, string firstName, string lastName,
        string? phoneNumber, string? address,
        DateTime? dateOfBirth, string? gender,
        string? licenseNumber, DateTime? licenseExpiryDate,
        string? specialization, int? yearsOfExperience,
        string? clinicName, string? clinicAddress,
        DateTime? employmentDate, Guid? companyId)
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