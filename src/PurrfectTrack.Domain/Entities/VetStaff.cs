namespace PurrfectTrack.Domain.Entities;

public class VetStaff : Contact
{
    public string? Position { get; set; }
    public string? Department { get; set; }
    public DateTime? EmploymentDate { get; set; }

    protected VetStaff() { }

    public VetStaff(Guid userId, string firstName, string lastName,
        string? phoneNumber = null, string? address = null,
        DateTime? dateOfBirth = null, string? gender = null,
        string? position = null, string? department = null,
        DateTime? employmentDate = null)
        : base(userId, firstName, lastName, phoneNumber, address, dateOfBirth, gender)
    {
        Position = position;
        Department = department;
        EmploymentDate = employmentDate;
    }
}