using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrfectTrack.Application.PetOwners.Commands.DeletePetOwner;
using PurrfectTrack.Application.Users.Commands.CreateUser;
using PurrfectTrack.Application.Users.Commands.DeleteUser;
using PurrfectTrack.Application.Users.Commands.UpdateUser;
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

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest("User ID mismatch");
        }

        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : NotFound();
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