namespace PurrfectTrack.Application.DTOs;

public class PetOwnerModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public UserModel User { get; set; } = new();

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }

    public int NumberOfPets { get; set; }

    public List<PetModel> Pets { get; set; } = new();
}
