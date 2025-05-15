// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        PetOwnersController
//  Created:     5/16/2025 7:32:19 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

[Route("api/[controller]")]
[ApiController]
public class PetOwnersController : ControllerBase
{
    private readonly IMediator _mediator;

    public PetOwnersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreatePetOwner([FromBody] CreatePetOwnerCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetPetOwnerById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Administrator,PetOwner")]
    public async Task<IActionResult> UpdatePetOwner(Guid id, [FromBody] UpdatePetOwnerCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest("Pet Owner ID mismatch");

        var result = await _mediator.Send(command, cancellationToken);
        if (result == null)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeletePetOwner(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeletePetOwnerCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : NotFound();
    }

    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> GetPetOwners(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPetOwnersQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("pet/{petId:guid}")]
    public async Task<IActionResult> GetPetOwnerByPet(Guid petId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPetOwnerByPetQuery(petId), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPetOwnerById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetPetOwnerByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("{id:guid}/upload-image")]
    [Authorize(Roles = "Administrator,PetOwner")]
    public async Task<IActionResult> UploadPetOwnerImage(Guid id, [FromForm] IFormFile file, CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Image file is required.");

        var command = new UploadPetOwnerImageCommand(id, file);
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }
}
