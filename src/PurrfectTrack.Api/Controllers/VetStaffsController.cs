// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        VetStaffsController
//  Created:     5/16/2025 7:32:19 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

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
        return CreatedAtAction(nameof(GetVetStaffById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Administrator,VetStaff")]
    public async Task<IActionResult> UpdateVetStaff(Guid id, [FromBody] UpdateVetStaffCommand command, CancellationToken cancellationToken)
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
    public async Task<IActionResult> DeleteVetStaff(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteVetStaffCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : NotFound();
    }

    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> GetVetStaffs(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetVetStaffsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("company/{companyId:guid}")]
    public async Task<IActionResult> GetVetStaffsByCompany([FromRoute] Guid companyId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetVetStaffsByCompanyQuery(companyId), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVetStaffById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetVetStaffByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("{id:guid}/upload-image")]
    [Authorize(Roles = "Administrator,VetStaff")]
    public async Task<IActionResult> UploadVetStaffImage(Guid id, [FromForm] IFormFile file, CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Image file is required.");

        var command = new UploadVetStaffImageCommand(id, file);
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }
}