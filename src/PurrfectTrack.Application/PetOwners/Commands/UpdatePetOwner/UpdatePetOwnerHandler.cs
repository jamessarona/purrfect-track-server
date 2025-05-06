using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.Exceptions;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.PetOwners.Commands.UpdatePetOwner;

public class UpdatePetOwnerHandler
    : BaseHandler, ICommandHandler<UpdatePetOwnerCommand, UpdatePetOwnerResult>
{
    public UpdatePetOwnerHandler(IApplicationDbContext dbContext) 
        : base(dbContext) { }

    public async Task<UpdatePetOwnerResult> Handle(UpdatePetOwnerCommand command, CancellationToken cancellationToken)
    {
        var petOwner = await dbContext.PetOwners
            .FirstOrDefaultAsync(po => po.Id == command.Id, cancellationToken);

        if (petOwner is null)
            throw new PetOwnerNotFoundException(command.Id);

        petOwner.FirstName = command.FirstName;
        petOwner.LastName = command.LastName;
        petOwner.Email = command.Email;
        petOwner.PhoneNumber = command.PhoneNumber ?? petOwner.PhoneNumber;
        petOwner.Address = command.Address ?? petOwner.Address;
        petOwner.DateOfBirth = command.DateOfBirth ?? petOwner.DateOfBirth;
        petOwner.Gender = command.Gender ?? petOwner.Gender;

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdatePetOwnerResult(petOwner.Id);
    }
}