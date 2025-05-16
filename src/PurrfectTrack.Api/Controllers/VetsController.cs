// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        VetsController
//  Created:     5/16/2025 7:32:19 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VetsController : ControllerBase
{
    private readonly IMediator _mediator;

    public VetsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateVet([FromBody] CreateVetCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetVetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Administrator,Vet")]
    public async Task<IActionResult> UpdateVet(Guid id, [FromBody] UpdateVetCommand command, CancellationToken cancellationToken)
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
        var result = await _mediator.Send(new DeleteVetCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : NotFound();
    }

    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> GetVets(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetVetsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("company/{companyId}")]
    public async Task<IActionResult> GetVetsByCompany([FromRoute] Guid companyId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetVetsByCompanyQuery(companyId), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetVetByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}