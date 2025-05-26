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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RefreshTokenHandler(
        IApplicationDbContext dbContext,
        IJwtService jwtService,
        IRefreshTokenService refreshTokenService,
        ILogger<RefreshTokenHandler> logger,
        IHttpContextAccessor httpContextAccessor)
        : base(dbContext)
    {
        _jwtService = jwtService;
        _refreshTokenService = refreshTokenService;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<RefreshTokenResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshTokenValue = request.RefreshToken;

        if (string.IsNullOrEmpty(refreshTokenValue))
        {
            _logger.LogWarning("Refresh token missing.");
            throw new UnauthorizedAccessException("Refresh token missing.");
        }

        var refreshToken = await dbContext.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == refreshTokenValue, cancellationToken);

        if (refreshToken == null || refreshToken.IsRevoked || (refreshToken.ExpiresAt <= DateTime.UtcNow))
        {
            _logger.LogWarning("Invalid or expired refresh token.");
            throw new UnauthorizedAccessException("Invalid refresh token.");
        }

        bool isRememberMe = false;

        if (refreshToken.ExpiresAt.HasValue && refreshToken.CreatedAt.HasValue)
        {
            var lifespan = refreshToken.ExpiresAt.Value - refreshToken.CreatedAt.Value;
            isRememberMe = lifespan.TotalDays > 7;
        }

        var newAccessTokenExpiry = isRememberMe
            ? DateTime.UtcNow.AddDays(30)
            : DateTime.UtcNow.AddMinutes(60);

        var newAccessToken = _jwtService.GenerateToken(
            refreshToken.UserId,
            refreshToken.User.Role,
            newAccessTokenExpiry
        );

        var newRefreshToken = _refreshTokenService.GenerateRefreshToken();

        refreshToken.IsRevoked = true;

        var replacement = new RefreshToken
        {
            Token = newRefreshToken,
            UserId = refreshToken.UserId,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = isRememberMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        dbContext.RefreshTokens.Add(replacement);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new RefreshTokenResult(newAccessToken, newRefreshToken, replacement.ExpiresAt.Value);
    }
}