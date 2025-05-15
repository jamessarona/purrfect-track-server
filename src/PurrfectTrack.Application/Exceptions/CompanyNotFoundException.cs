using PurrfectTrack.Shared.Exceptions;

namespace PurrfectTrack.Application.Exceptions;

class CompanyNotFoundException : NotFoundException
{
    public CompanyNotFoundException(Guid id) : base("Company", id)
    { 
    }
}
