namespace PurrfectTrack.Domain.Entities;

public class Pet : Entity<Guid>
{
    [Required]
    public Guid OwnerId { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    public string? Species { get; set; }
    public string? Breed { get; set; }
    public string? Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public float? Weight { get; set; }
    public string? Color { get; set; }
    public bool? IsNeutered { get; set; }

    public PetOwner Owner { get; set; } = default!;

    protected Pet() { }

    public Pet(Guid ownerId, string name, string? species = null, string? breed = null, string? gender = null,
        DateTime? dateOfBirth = null, float? weight = null, string? color = null, bool? isNeutered = null)
    {
        Id = Guid.NewGuid();
        OwnerId = ownerId;
        Name = name;
        Species = species;
        Breed = breed;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        Weight = weight;
        Color = color;
        IsNeutered = isNeutered;
    }
}