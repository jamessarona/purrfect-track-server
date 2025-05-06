using MediatR; 
using Microsoft.AspNetCore.Mvc;
using PurrfectTrack.Application.Pets.Commands.CreatePet;
using PurrfectTrack.Application.Pets.Commands.DeletePet;
using PurrfectTrack.Application.Pets.Commands.UpdatePet;
using PurrfectTrack.Application.Pets.Queries.GetPetById;
using PurrfectTrack.Application.Pets.Queries.GetPets;
using PurrfectTrack.Application.Pets.Queries.GetPetsByOwner;

namespace PurrfectTrack.Api.Controllers;

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
    public async Task<IActionResult> CreatePet([FromBody] CreatePetCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetPetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePet(Guid id, [FromBody] UpdatePetCommand command)
    {
        if (id != command.Id)
            return BadRequest("Pet ID mismatch");

        var result = await _mediator.Send(command);
        if (result == null)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeletePetCommand(id));

        return result.IsSuccess ? NoContent() : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetPets()
    {
        var result = await _mediator.Send(new GetPetsQuery());
        return Ok(result);
    }

    [HttpGet("owner/{ownerId:guid}")]
    public async Task<IActionResult> GetPetsByOwner(Guid ownerId)
    {
        var result = await _mediator.Send(new GetPetsByOwnerQuery(ownerId));
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPetById(Guid id)
    {
        var query = new GetPetByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}
