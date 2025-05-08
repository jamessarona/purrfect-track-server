using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrfectTrack.Application.PetOwners.Commands.CreatePetOwner;
using PurrfectTrack.Application.PetOwners.Commands.DeletePetOwner;
using PurrfectTrack.Application.PetOwners.Commands.UpdatePetOwner;
using PurrfectTrack.Application.PetOwners.Queries.GetPetOwnerById;
using PurrfectTrack.Application.PetOwners.Queries.GetPetOwnerByPet;
using PurrfectTrack.Application.PetOwners.Queries.GetPetOwners;

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
}
