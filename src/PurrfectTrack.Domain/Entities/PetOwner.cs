using System.ComponentModel.DataAnnotations;

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

    private readonly List<Pet> _pets = new();
    public IReadOnlyList<Pet> Pets => _pets.AsReadOnly();

    public int NumberOfPets => _pets.Count;

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
    {
        get => Pets.Count;
        private set { }
    }
}
