using PurrfectTrack.Application.Data;
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
        var petOwner = new PetOwner(
            command.FirstName,
            command.LastName,
            command.Email,
            command.PhoneNumber,
            command.Address,
            command.DateOfBirth,
            command.Gender);

        dbContext.PetOwners.Add(petOwner);

        await dbContext.SaveChangesAsync(cancellationToken);


        return new CreatePetOwnerResult(petOwner.Id);
    }
}