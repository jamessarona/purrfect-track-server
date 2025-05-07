using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.Exceptions;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;
using PurrfectTrack.Shared.Security;

namespace PurrfectTrack.Application.Users.Commands.UpdateUser;

public class UpdateUserHandler
    : BaseHandler, ICommandHandler<UpdateUserCommand, UpdateUserResult>
{
    private readonly IPasswordHasher _passwordHasher;

    public UpdateUserHandler(IApplicationDbContext dbContext, IPasswordHasher passwordHasher)
        : base(dbContext)
    {
        _passwordHasher = passwordHasher;
    }

    public async Task<UpdateUserResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == command.Id, cancellationToken);

        if (user is null)
            throw new UserNotFoundException(command.Id);

        user.Email = command.Email;
        user.Role = command.Role;
        user.IsActive = command.IsActive;

        if (!string.IsNullOrWhiteSpace(command.Password))
        {
            user.PasswordHash = _passwordHasher.Hash(command.Password);
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateUserResult(user.Id);
    }
}