// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        RefreshTokenHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Users.Commands.RefreshTokenFeature;

public class RefreshTokenHandler : BaseHandler, ICommandHandler<RefreshTokenCommand, RefreshTokenResult>
{
    private readonly IJwtService _jwtService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly ILogger<RefreshTokenHandler> _logger;

    public RefreshTokenHandler(IApplicationDbContext dbContext, IJwtService jwtService, 
            IRefreshTokenService refreshTokenService, ILogger<RefreshTokenHandler> logger)
        : base(dbContext)
    {
        _jwtService = jwtService;
        _refreshTokenService = refreshTokenService;
        _logger = logger;
    }

    public async Task<RefreshTokenResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await dbContext.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken, cancellationToken);

        if (refreshToken is null || refreshToken.IsRevoked || refreshToken.ExpiresAt <= DateTime.UtcNow)
        {
            _logger.LogWarning("Invalid or expired refresh token.");
            throw new UnauthorizedAccessException("Invalid refresh token.");
        }

        var newAccessToken = _jwtService.GenerateToken(refreshToken.UserId, refreshToken.User.Role);
        var newRefreshToken = _refreshTokenService.GenerateRefreshToken();

        refreshToken.IsRevoked = true;
        var replacement = new RefreshToken
        {
            Token = newRefreshToken,
            UserId = refreshToken.UserId,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            ReplacedByToken = null,
            IsRevoked = false
        };

        dbContext.RefreshTokens.Add(replacement);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new RefreshTokenResult(newAccessToken, newRefreshToken);
    }
}