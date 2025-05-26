// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        LoginHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Users.Commands.Login;

public class LoginHandler 
    : BaseHandler, ICommandHandler<LoginCommand, LoginResult>
{
    private readonly IJwtService _jwtService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<LoginHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRefreshTokenService _refreshTokenService;

    public LoginHandler(IApplicationDbContext dbContext, IJwtService jwtService, 
            IPasswordHasher passwordHasher, ILogger<LoginHandler> logger, 
            IHttpContextAccessor httpContextAccessor, IRefreshTokenService refreshTokenService)
        : base(dbContext)
    {
        _jwtService = jwtService;
        _passwordHasher = passwordHasher;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _refreshTokenService = refreshTokenService;
    }

    public async Task<LoginResult> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await GetUserByEmailAsync(command.Email, cancellationToken);

        ValidateUserPassword(user, command.Password);

        var now = DateTime.UtcNow;
        var tokenExpiryMinutes = command.RememberMe ? 60 * 24 * 30 : 60;

        var token = _jwtService.GenerateToken(user.Id, user.Role, now.AddMinutes(tokenExpiryMinutes));

        var refreshToken = _refreshTokenService.GenerateRefreshToken();

        var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        var userAgent = _httpContextAccessor.HttpContext?.Request?.Headers["User-Agent"].ToString();

        if (ip == null)
            _logger.LogInformation("IP address could not be retrieved");
        if (string.IsNullOrEmpty(userAgent))
            _logger.LogInformation("User-Agent header missing");

        var session = new UserSession
        {
            UserId = user.Id,
            Token = token,
            CreatedAt = now,
            ExpiresAt = now.AddMinutes(tokenExpiryMinutes),
            IpAddress = ip,
            UserAgent = userAgent
        };

        dbContext.UserSessions.Add(session);

        var refreshTokenExpiryDays = command.RememberMe ? 30 : 7;

        var refreshTokenEntity = new RefreshToken
        {
            UserId = user.Id,
            Token = refreshToken,
            CreatedAt = now,
            ExpiresAt = now.AddDays(refreshTokenExpiryDays),
            IpAddress = ip,
            UserAgent = userAgent
        };

        dbContext.RefreshTokens.Add(refreshTokenEntity);

        await dbContext.SaveChangesAsync(cancellationToken);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = now.AddMinutes(tokenExpiryMinutes)
        };

        if (command.RememberMe)
        {
            cookieOptions.Expires = now.AddDays(30);
        }

        return new LoginResult(token, session.Id, refreshToken);
    }

    private async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

        if (user == null)
        {
            _logger.LogWarning("Login failed for user {Email}: User not found", email);
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        return user;
    }

    private void ValidateUserPassword(User user, string password)
    {
        var passwordValid = _passwordHasher.Verify(password, user.PasswordHash);

        if (!passwordValid)
        {
            _logger.LogWarning("Login failed for user {Email}: Invalid password", user.Email);
            throw new UnauthorizedAccessException("Invalid credentials");
        }
    }
}