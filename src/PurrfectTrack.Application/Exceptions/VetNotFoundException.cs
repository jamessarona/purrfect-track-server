using PurrfectTrack.Shared.Exceptions;

namespace PurrfectTrack.Application.Exceptions;

public class VetNotFoundException : NotFoundException
{
    public VetNotFoundException(Guid id) : base("Vet", id)
    {
    }
}