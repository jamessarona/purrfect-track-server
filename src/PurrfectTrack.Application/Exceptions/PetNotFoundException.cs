using PurrfectTrack.Shared.Exceptions;

namespace PurrfectTrack.Application.Exceptions;

class PetNotFoundException : NotFoundException
{
    public PetNotFoundException(Guid id) : base("Pet", id)
    {
    }
}
