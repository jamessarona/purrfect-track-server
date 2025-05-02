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
    public void CreatePet(Guid petOwnerId, string name, string? species = null, string? breed = null,
        string? gender = null, DateTime? dateOfBirth = null, float? weight = null,
        string? color = null, bool? isNeutered = null)
    {
        var pet = new Pet(
            petOwnerId,
            name,
            species,
            breed,
            gender,
            dateOfBirth,
            weight,
            color,
            isNeutered);

        _pets.Add(pet);
    }

    public void UpdatePet(Guid petId, string? name = null, string? species = null, string? breed = null,
        string? gender = null, DateTime? dateOfBirth = null, float? weight = null,
        string? color = null, bool? isNeutered = null)
    {
        var pet = _pets.FirstOrDefault(p => p.Id == petId);
        if (pet is null)
            throw new InvalidOperationException("Pet not found.");

        if (!string.IsNullOrWhiteSpace(name)) pet.Name = name;
        if (!string.IsNullOrWhiteSpace(species)) pet.Species = species;
        if (!string.IsNullOrWhiteSpace(breed)) pet.Breed = breed;
        if (!string.IsNullOrWhiteSpace(gender)) pet.Gender = gender;
        if (dateOfBirth.HasValue) pet.DateOfBirth = dateOfBirth;
        if (weight.HasValue) pet.Weight = weight;
        if (!string.IsNullOrWhiteSpace(color)) pet.Color = color;
        if (isNeutered.HasValue) pet.IsNeutered = isNeutered;
    }

    public void DeletePet(Guid petId)
    {
        var pet = _pets.FirstOrDefault(p => p.Id == petId);
        if (pet is null)
            throw new InvalidOperationException("Pet not found.");

        _pets.Remove(pet);
    }
}
