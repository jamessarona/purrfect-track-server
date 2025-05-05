using PurrfectTrack.Shared.Exceptions;

namespace PurrfectTrack.Application.Exceptions;

class OwnerNotFoundException : NotFoundException
{
    public OwnerNotFoundException(Guid id) : base("Pet Owner", id)
    {
    }
}
