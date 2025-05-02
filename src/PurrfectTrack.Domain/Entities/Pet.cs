using System.ComponentModel.DataAnnotations;

namespace PurrfectTrack.Domain.Entities;

public class Pet : Entity<Guid>
{
    [Required]
    public Guid PetOwnerId { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    public string? Species { get; set; }
    public string? Breed { get; set; }
    public string? Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public float? Weight { get; set; }
    public string? Color { get; set; }
    public bool? IsNeutered { get; set; }

    protected Pet() { }

    public Pet(Guid petOwnerId, string name, string? species = null, string? breed = null, string? gender = null,
        DateTime? dateOfBirth = null, float? weight = null, string? color = null, bool? isNeutered = null)
    {
        Id = Guid.NewGuid();
        PetOwnerId = petOwnerId;
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