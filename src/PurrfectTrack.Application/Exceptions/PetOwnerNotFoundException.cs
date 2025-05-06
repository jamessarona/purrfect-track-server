using PurrfectTrack.Shared.Exceptions;

namespace PurrfectTrack.Application.Exceptions;

class PetOwnerNotFoundException : NotFoundException
{
    public PetOwnerNotFoundException(Guid id) : base("Pet Owner", id)
    {
    }
}
