// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        LogoutHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

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