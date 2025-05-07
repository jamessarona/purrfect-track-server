using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.PetOwners.Commands.DeletePetOwner;

public class DeletePetOwnerHandler
    : BaseHandler, ICommandHandler<DeletePetOwnerCommand, DeletePetOwnerResult>
{
    public DeletePetOwnerHandler(IApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<DeletePetOwnerResult> Handle(DeletePetOwnerCommand command, CancellationToken cancellationToken)
    {
        var petOwner = await dbContext.PetOwners
            .FirstOrDefaultAsync(po => po.Id == command.Id, cancellationToken);

        if (petOwner is null)
            return new DeletePetOwnerResult(false);

        dbContext.PetOwners.Remove(petOwner);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeletePetOwnerResult(true);
    }
}