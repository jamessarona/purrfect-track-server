using MediatR;
using Microsoft.AspNetCore.Mvc;
using PurrfectTrack.Application.Pets.Commands.CreatePet;
using PurrfectTrack.Application.Pets.Commands.DeletePet;
using PurrfectTrack.Application.Pets.Commands.UpdatePet;
using PurrfectTrack.Application.Pets.Queries.GetPetById;
using PurrfectTrack.Application.Pets.Queries.GetPets;
using PurrfectTrack.Application.Pets.Queries.GetPetsByOwner;

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
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeletePetCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : NotFound();
    }

    [HttpGet]
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
}