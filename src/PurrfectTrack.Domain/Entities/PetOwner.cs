namespace PurrfectTrack.Domain.Entities;

public class PetOwner : Contact
{
    public List<Pet> Pets { get; private set; } = new();

    public int NumberOfPets => Pets.Count;

    protected PetOwner() { }

    public PetOwner(Guid userId, string firstName, string lastName,
        string? phoneNumber = null, string? address = null,
        DateTime? dateOfBirth = null, string? gender = null)
        : base(userId, firstName, lastName, phoneNumber, address, dateOfBirth, gender)
    { }
}
