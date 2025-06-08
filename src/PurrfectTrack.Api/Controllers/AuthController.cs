// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        AuthController
//  Created:     5/16/2025 7:32:19 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IWebHostEnvironment _env;

    public AuthController(ISender mediator, IWebHostEnvironment env)
    {
        _mediator = mediator;
        _env = env;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);

        var cookieOptions = CreateCookieOptions(command.RememberMe);

        Response.Cookies.Append("access_token", result.Token, cookieOptions);
        Response.Cookies.Append("refresh_token", result.RefreshToken, cookieOptions);

        return Ok(new { SessionId = result.SessionId });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var token = Request.Cookies["access_token"];
        if (string.IsNullOrEmpty(token))
            return BadRequest("Authorization token is missing");

        await _mediator.Send(new LogoutCommand(token));

        var cookieOptions = CreateCookieOptions();

        Response.Cookies.Delete("access_token", cookieOptions);
        Response.Cookies.Delete("refresh_token", cookieOptions);

        return NoContent();
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refresh_token"];
        if (string.IsNullOrEmpty(refreshToken))
            return Unauthorized("Refresh token is missing.");

        var result = await _mediator.Send(new RefreshTokenCommand(refreshToken));

        var cookieOptions = CreateCookieOptions(expires: result.RefreshTokenExpiresAt);

        Response.Cookies.Append("access_token", result.AccessToken, cookieOptions);
        Response.Cookies.Append("refresh_token", result.RefreshToken, cookieOptions);

        return Ok(new { Success = true });
    }

    [HttpGet("session")]
    [Authorize]
    public IActionResult CheckSession()
    {
        return Ok(new { message = "Session valid" });
    }

    private CookieOptions CreateCookieOptions(bool rememberMe = false, DateTimeOffset? expires = null)
    {
        var isDevelopment = _env.IsDevelopment();

        var options = new CookieOptions
        {
            HttpOnly = true,
            Secure = !isDevelopment,
            SameSite = isDevelopment ? SameSiteMode.Lax : SameSiteMode.None,
            Path = "/",
            IsEssential = true
        };

        if (rememberMe && !expires.HasValue)
        {
            options.Expires = DateTime.UtcNow.AddDays(30);
        }
        else if (expires.HasValue)
        {
            options.Expires = expires.Value.UtcDateTime;
        }

        return options;
    }

}
