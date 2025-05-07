namespace PurrfectTrack.Domain.Entities;

public class PetOwner : Entity<Guid>
{
    [Required]
    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    [Required]
    public string FirstName { get; set; } = default!;

    [Required]
    public string LastName { get; set; } = default!;

    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }

    public List<Pet> Pets { get; private set; } = new();

    public int NumberOfPets => Pets.Count;

    protected PetOwner() { }

    public PetOwner(Guid userId, string firstName, string lastName,
        string? phoneNumber = null, string? address = null,
        DateTime? dateOfBirth = null, string? gender = null)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Address = address;
        DateOfBirth = dateOfBirth;
        Gender = gender;
    }
}
