using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.Exceptions;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Pets.Commands.UpdatePet;

public class UpdatePetHandler
    : BaseHandler, ICommandHandler<UpdatePetCommand, UpdatePetResult>
{
    public UpdatePetHandler(IApplicationDbContext dbContext) 
        : base(dbContext) { }

    public async Task<UpdatePetResult> Handle(UpdatePetCommand command, CancellationToken cancellationToken)
    {
        var pet = await dbContext.Pets
            .Include(p => p.PetOwner)
            .FirstOrDefaultAsync(p => p.Id == command.Id, cancellationToken);

        if (pet is null)
            throw new PetNotFoundException(command.Id);

        if (pet.PetOwnerId != command.PetOwnerId)
            throw new InvalidOperationException("The Pet does not belong to the specified Pet Owner.");

        pet.Name = command.Name ?? pet.Name;
        pet.Species = command.Species ?? pet.Species;
        pet.Breed = command.Breed ?? pet.Breed;
        pet.Gender = command.Gender ?? pet.Gender;
        pet.DateOfBirth = command.DateOfBirth ?? pet.DateOfBirth;
        pet.Weight = command.Weight ?? pet.Weight;
        pet.Color = command.Color ?? pet.Color;
        pet.IsNeutered = command.IsNeutered ?? pet.IsNeutered;

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdatePetResult(pet.Id);
    }
}