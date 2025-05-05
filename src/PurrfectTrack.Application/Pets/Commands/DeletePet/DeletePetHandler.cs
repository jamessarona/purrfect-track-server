using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Pets.Commands.DeletePet;


public class DeletePetHandler
    : BaseHandler, ICommandHandler<DeletePetCommand, DeletePetResult>
{
    public DeletePetHandler(IApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<DeletePetResult> Handle(DeletePetCommand command, CancellationToken cancellationToken)
    {
        var pet = await dbContext.Pets.FirstOrDefaultAsync(p => p.Id == command.Id, cancellationToken);

        if (pet is null)
            return new DeletePetResult(false);

        dbContext.Pets.Remove(pet);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeletePetResult(true);
    }
}