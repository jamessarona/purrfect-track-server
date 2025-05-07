using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;
using PurrfectTrack.Shared.Security;

namespace PurrfectTrack.Application.Users.Commands.CreateUser;

public class CreateUserHandler
    : BaseHandler, ICommandHandler<CreateUserCommand, CreateUserResult>
{
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserHandler(IApplicationDbContext dbContext, IPasswordHasher passwordHasher)
        : base(dbContext)
    {
        _passwordHasher = passwordHasher;
    }

    public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var emailExists = await dbContext.Users
            .AnyAsync(u => u.Email == command.Email, cancellationToken);

        if (emailExists)
            throw new InvalidOperationException($"Email '{command.Email}' is already in use.");

        var hashedPassword = _passwordHasher.Hash(command.Password);

        var user = new User(
            command.Email,
            hashedPassword,
            command.Role
        );

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateUserResult(user.Id);
    }
}