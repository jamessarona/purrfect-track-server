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

    protected Vet() { }

    public Vet(Guid userId, string firstName, string lastName,
        string? phoneNumber = null, string? address = null,
        DateTime? dateOfBirth = null, string? gender = null,
        string? licenseNumber = null, DateTime? licenseExpiryDate = null,
        string? specialization = null, int? yearsOfExperience = null,
        string? clinicName = null, string? clinicAddress = null,
        DateTime? employmentDate = null)
        : base(userId, firstName, lastName, phoneNumber, address, dateOfBirth, gender)
    {
        LicenseNumber = licenseNumber;
        LicenseExpiryDate = licenseExpiryDate;
        Specialization = specialization;
        YearsOfExperience = yearsOfExperience;
        ClinicName = clinicName;
        ClinicAddress = clinicAddress;
        EmploymentDate = employmentDate;
    }
}