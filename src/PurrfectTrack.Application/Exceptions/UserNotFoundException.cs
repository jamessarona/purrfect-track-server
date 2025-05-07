using PurrfectTrack.Shared.Exceptions;

namespace PurrfectTrack.Application.Exceptions;

class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(Guid id) : base("User", id)
    {
    }
}
