using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;
using PurrfectTrack.Shared.Security;

namespace PurrfectTrack.Application.Users.Commands.Login;

public class LoginHandler 
    : BaseHandler, ICommandHandler<LoginCommand, LoginResult>
{
    private readonly IJwtService _jwtService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<LoginHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LoginHandler(IApplicationDbContext dbContext, IJwtService jwtService, 
            IPasswordHasher passwordHasher, ILogger<LoginHandler> logger, 
            IHttpContextAccessor httpContextAccessor)
        : base(dbContext)
    {
        _jwtService = jwtService;
        _passwordHasher = passwordHasher;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<LoginResult> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await GetUserByEmailAsync(command.Email, cancellationToken);

        ValidateUserPassword(user, command.Password);

        var token = _jwtService.GenerateToken(user.Id, user.Role);

        var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        var userAgent = _httpContextAccessor.HttpContext?.Request?.Headers["User-Agent"].ToString();

        if (ip == null) 
            _logger.LogInformation("IP address could not be retrieved");
        if (string.IsNullOrEmpty(userAgent)) 
            _logger.LogInformation("User-Agent header missing");

        var now = DateTime.UtcNow;
        var session = new UserSession
        {
            UserId = user.Id,
            Token = token,
            CreatedAt = now,
            ExpiresAt = now.AddMinutes(60),
            IpAddress = ip,
            UserAgent = userAgent
        };

        dbContext.UserSessions.Add(session);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new LoginResult(token, session.Id);
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