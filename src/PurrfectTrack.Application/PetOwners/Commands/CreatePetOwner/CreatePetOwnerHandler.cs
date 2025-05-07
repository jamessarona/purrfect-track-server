using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.Exceptions;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.PetOwners.Commands.CreatePetOwner;

public class CreatePetOwnerHandler
    : BaseHandler, ICommandHandler<CreatePetOwnerCommand, CreatePetOwnerResult>
{
    public CreatePetOwnerHandler(IApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<CreatePetOwnerResult> Handle(CreatePetOwnerCommand command, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);

        if (user == null)
            throw new UserNotFoundException(command.UserId);

        var petOwner = new PetOwner(
            command.UserId,
            command.FirstName,
            command.LastName,
            command.PhoneNumber,
            command.Address,
            command.DateOfBirth,
            command.Gender);

        dbContext.PetOwners.Add(petOwner);

        await dbContext.SaveChangesAsync(cancellationToken);


        return new CreatePetOwnerResult(petOwner.Id);
    }
}