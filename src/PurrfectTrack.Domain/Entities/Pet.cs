using System.ComponentModel.DataAnnotations;

namespace PurrfectTrack.Domain.Entities;

public class Pet : Entity<Guid>
{
    internal Pet(Guid petOwnerId, string name, string species, string breed, string gender, 
        DateTime dateOfBirth, float weight, string color, bool isNeutered)
    {
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

    [Required]
    public Guid PetOwnerId { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Species { get; set; }
    public string? Breed { get; set; }
    public string? Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public float? Weight { get; set; }
    public string? Color { get; set; }
    public bool? IsNeutered { get; set; }
}
