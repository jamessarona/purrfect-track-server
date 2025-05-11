using PurrfectTrack.Shared.Exceptions;

namespace PurrfectTrack.Application.Exceptions;

public class VetStaffNotFoundException : NotFoundException
{
    public VetStaffNotFoundException(Guid id) : base("Vet", id)
    {
    }
}