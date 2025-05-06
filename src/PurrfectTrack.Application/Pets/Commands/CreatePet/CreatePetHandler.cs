using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.Exceptions;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Pets.Commands.CreatePet;

public class CreatePetHandler
    : BaseHandler, ICommandHandler<CreatePetCommand, CreatePetResult>
{
    public CreatePetHandler(IApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<CreatePetResult> Handle(CreatePetCommand command, CancellationToken cancellationToken)
    {
        var ownerExists = await dbContext.PetOwners
        .AnyAsync(o => o.Id == command.PetOwnerId, cancellationToken);

        if (!ownerExists)
            throw new PetOwnerNotFoundException(command.PetOwnerId);

        var newPet = new Pet(
            command.PetOwnerId,
            command.Name,
            command.Species,
            command.Breed,
            command.Gender,
            command.DateOfBirth,
            command.Weight,
            command.Color,
            command.IsNeutered
        );

        dbContext.Pets.Add(newPet);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreatePetResult(newPet.Id);
    }
}