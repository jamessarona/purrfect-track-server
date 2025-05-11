using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrfectTrack.Application.VetStaffs.Commands.CreateVetStaff;
using PurrfectTrack.Application.VetStaffs.Commands.DeleteVetStaff;
using PurrfectTrack.Application.VetStaffs.Commands.UpdateVetStaff;
using PurrfectTrack.Application.VetStaffs.Queries.GetVetStaffById;
using PurrfectTrack.Application.VetStaffs.Queries.GetVetStaffs;

namespace PurrfectTrack.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VetStaffsController : ControllerBase
{
    private readonly IMediator _mediator;

    public VetStaffsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateVetStaff([FromBody] CreateVetStaffCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetVetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Administrator,VetStaff")]
    public async Task<IActionResult> UpdateVet(Guid id, [FromBody] UpdateVetStaffCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest("Vet ID mismatch");

        var result = await _mediator.Send(command, cancellationToken);
        if (result == null)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteVet(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteVetStaffCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : NotFound();
    }

    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> GetVets(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetVetStaffsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetVetStaffByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}