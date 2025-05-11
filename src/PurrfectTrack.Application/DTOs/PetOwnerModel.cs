namespace PurrfectTrack.Application.DTOs;

public class PetOwnerModel : ContactModel
{
    public List<PetModel> Pets { get; set; } = new();
}
