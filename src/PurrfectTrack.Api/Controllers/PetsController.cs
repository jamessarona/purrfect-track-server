// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        PetsController
//  Created:     5/16/2025 7:32:19 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

[Route("api/[controller]")]
[ApiController]
public class PetsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PetsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "Administrator,PetOwner")]
    public async Task<IActionResult> CreatePet([FromBody] CreatePetCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetPetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePet(Guid id, [FromBody] UpdatePetCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest("Pet ID mismatch");

        var result = await _mediator.Send(command, cancellationToken);
        if (result == null)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator,PetOwner")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeletePetCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : NotFound();
    }

    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> GetPets(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPetsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("owner/{ownerId:guid}")]
    public async Task<IActionResult> GetPetsByOwner(Guid ownerId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPetsByOwnerQuery(ownerId), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetPetByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("{id:guid}/upload-image")]
    [Authorize(Roles = "Administrator,PetOwner")]
    public async Task<IActionResult> UploadPetImage(Guid id, [FromForm] IFormFile file, CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Image file is required.");

        var command = new UploadPetImageCommand(id, file);
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }
}