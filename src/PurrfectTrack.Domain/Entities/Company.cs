namespace PurrfectTrack.Domain.Entities;

public class Company : Entity<Guid>
{
    [Required]
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? Address { get; set; }
    public string? TaxpayerId { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<Vet> Vets { get; set; } = new List<Vet>();
    public ICollection<VetStaff> VetStaffs { get; set; } = new List<VetStaff>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    protected Company() { }

    public Company(string name, string? description, string? phoneNumber, string? email, string? website, string? address, string? taxpayerId, bool isActive)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        PhoneNumber = phoneNumber;
        Email = email;
        Website = website;
        Address = address;
        TaxpayerId = taxpayerId;
        IsActive = isActive;
    }
}