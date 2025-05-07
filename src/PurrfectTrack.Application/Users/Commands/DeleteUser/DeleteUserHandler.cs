using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.Exceptions;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Users.Commands.DeleteUser;

public class DeleteUserHandler 
    : BaseHandler, ICommandHandler<DeleteUserCommand, DeleteUserResult>
{
    public DeleteUserHandler(IApplicationDbContext dbContext) 
        : base(dbContext) { }

    public async Task<DeleteUserResult> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .Include(u => u.PetOwnerProfile) 
            .FirstOrDefaultAsync(u => u.Id == command.Id, cancellationToken);

        if (user is null)
            throw new UserNotFoundException(command.Id); 

        if (user.PetOwnerProfile != null)
            dbContext.PetOwners.Remove(user.PetOwnerProfile);

        dbContext.Users.Remove(user);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteUserResult(true);
    }
}