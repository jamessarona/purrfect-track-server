// Not yet done. sample only
namespace PurrfectTrack.Application.Services;

public class PetService
{
    private readonly IAuditService _auditService;

    public PetService(IAuditService auditService)
    {
        _auditService = auditService;
    }

    public Pet CreatePet(Guid ownerId, string name)
    {
        var pet = new Pet(ownerId, name);
        pet.SetCreated(_auditService.GetCurrentUser());

        // Save to DB (via repository, not shown here)
        return pet;
    }

    public void UpdatePet(Pet pet, string newName)
    {
        //pet.UpdateName(newName);
        pet.SetModified(_auditService.GetCurrentUser());

        // Save to DB (via repository, not shown here)
    }
}