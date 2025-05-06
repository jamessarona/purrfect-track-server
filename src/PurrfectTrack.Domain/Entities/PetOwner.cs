namespace PurrfectTrack.Domain.Entities;

public class PetOwner : Entity<Guid>
{
    [Required]
    public string FirstName { get; set; } = default!;

    [Required]
    public string LastName { get; set; } = default!;
    
    [Required]
    public string Email { get; set; } = default!;

    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }

    public List<Pet> Pets { get; private set; } = new();

    public int NumberOfPets => Pets.Count;

    protected PetOwner() { }

    public PetOwner(string firstName, string lastName, string email, string? phoneNumber = null,
        string? address = null, DateTime? dateOfBirth = null, string? gender = null)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
        DateOfBirth = dateOfBirth;
        Gender = gender;
    }
}
