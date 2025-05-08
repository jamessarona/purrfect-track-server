using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Users.Commands.Logout;

public class LogoutHandler 
    : BaseHandler, ICommandHandler<LogoutCommand>
{
    private readonly ILogger<LogoutHandler> _logger;

    public LogoutHandler(IApplicationDbContext dbContext, ILogger<LogoutHandler> logger)
        : base(dbContext)
    {
        _logger = logger;
    }

    public async Task<Unit> Handle(LogoutCommand command, CancellationToken cancellationToken)
    {
        var session = await dbContext.UserSessions
            .FirstOrDefaultAsync(us => us.Token == command.Token && !us.IsRevoked, cancellationToken);

        if (session != null)
        {
            session.IsRevoked = true;
            await dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Session for token {Token} has been successfully revoked.", command.Token);
        }
        else
        {
            _logger.LogWarning("Session with token {Token} not found or already revoked.", command.Token);
        }

        return Unit.Value;
    }
}