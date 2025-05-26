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

    public AuthController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTime.UtcNow.AddMinutes(command.RememberMe ? 60 * 24 * 30 : 60),
            SameSite = SameSiteMode.Strict,
            Path = "/"
        };

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

        var logoutCommand = new LogoutCommand(token);
        await _mediator.Send(logoutCommand);

        Response.Cookies.Delete("access_token");
        Response.Cookies.Delete("refresh_token");

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

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = result.RefreshTokenExpiresAt
        };

        Response.Cookies.Append("access_token", result.AccessToken, cookieOptions);
        Response.Cookies.Append("refresh_token", result.RefreshToken, cookieOptions);

        return Ok(new { Success = true });
    }
}