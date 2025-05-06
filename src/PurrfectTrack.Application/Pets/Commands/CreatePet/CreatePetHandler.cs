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
        var owner = await dbContext.PetOwners
            .Include(o => o.Pets)
            .FirstOrDefaultAsync(o => o.Id == command.OwnerId, cancellationToken);

        if (owner is null)
            throw new PetOwnerNotFoundException(command.OwnerId);

        owner.CreatePet(
            command.OwnerId,
            command.Name,
            command.Breed,
            command.Species,
            command.Gender,
            command.DateOfBirth,
            command.Weight,
            command.Color,
            command.IsNeutered
        );

        await dbContext.SaveChangesAsync(cancellationToken);

        var createdPet = owner.Pets.Last();
        if (createdPet == null)
            throw new Exception("Failed to create a pet.");

        return new CreatePetResult(createdPet.Id);
    }
}