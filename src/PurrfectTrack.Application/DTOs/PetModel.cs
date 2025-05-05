namespace PurrfectTrack.Application.DTOs;

public class PetModel
{
    public Guid? Id { get; set; }
    public Guid OwnerId { get; set; }
    public string Name { get; set; }
    public string? Species { get; set; }
    public string? Breed { get; set; }
    public string? Gender { get; set; }
    public DateTime? DateOfBirth { get; set; } 
    public float? Weight { get; set; }
    public string? Color { get; set; }
    public bool? IsNeutered { get; set; }
}