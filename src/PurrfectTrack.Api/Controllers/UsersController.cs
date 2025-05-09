using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrfectTrack.Application.Users.Queries.GetCurrentUser;
using PurrfectTrack.Application.Users.Queries.GetUserById;
using PurrfectTrack.Application.Users.Queries.GetUsers;
using PurrfectTrack.Application.Users.Queries.GetUsersByRole;
using PurrfectTrack.Shared.Pagination;
using System.Threading;

namespace PurrfectTrack.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mdiator)
    {
        _mediator = mdiator;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result.User);
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCurrentUserQuery(), cancellationToken);
        return Ok(result.User);
    }

    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 20)
    {
        var query = new GetUsersQuery(new PaginationRequest(pageIndex, pageSize));
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result.Users);
    }

    [HttpGet("role")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> GetUsersByRole(CancellationToken cancellationToken, [FromQuery] string role, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 20)
    {
        var query = new GetUsersByRoleQuery(role, new PaginationRequest(pageIndex, pageSize));
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result.Users);
    }
}