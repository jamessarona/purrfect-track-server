using MediatR;
using Microsoft.AspNetCore.Mvc;
using PurrfectTrack.Application.PetOwners.Commands.CreatePetOwner;
using PurrfectTrack.Application.PetOwners.Commands.DeletePetOwner;
using PurrfectTrack.Application.PetOwners.Commands.UpdatePetOwner;
using PurrfectTrack.Application.PetOwners.Queries.GetPetOwnerById;
using PurrfectTrack.Application.PetOwners.Queries.GetPetOwnerByPet;
using PurrfectTrack.Application.PetOwners.Queries.GetPetOwners;

namespace PurrfectTrack.Api.Controllers;

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
    public async Task<IActionResult> CreatePetOwner([FromBody] CreatePetOwnerCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetPetOwnerById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePetOwner(Guid id, [FromBody] UpdatePetOwnerCommand command)
    {
        if (id != command.Id)
            return BadRequest("Pet Owner ID mismatch");

        var result = await _mediator.Send(command);
        if (result == null)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePetOwner(Guid id)
    {
        var result = await _mediator.Send(new DeletePetOwnerCommand(id));

        return result.IsSuccess ? NoContent() : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetPetOwners()
    {
        var result = await _mediator.Send(new GetPetOwnersQuery());
        return Ok(result);
    }

    [HttpGet("pet/{petId:guid}")]
    public async Task<IActionResult> GetPetOwnerByPet(Guid petId)
    {
        var result = await _mediator.Send(new GetPetOwnerByPetQuery(petId));
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPetOwnerById(Guid id)
    {
        var query = new GetPetOwnerByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}
