// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        DeletePetHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

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