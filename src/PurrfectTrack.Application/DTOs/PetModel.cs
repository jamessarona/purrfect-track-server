namespace PurrfectTrack.Application.DTOs;

public class PetModel
{
    public int? Id { get; set; }
    public int OwnerId { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    public string? Breed { get; set; }
}