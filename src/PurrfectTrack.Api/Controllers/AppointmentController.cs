// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        AppointmentController
//  Created:     5/17/2025 10:44:03 PM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public AppointmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetAppointmentById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAppointment(Guid id, [FromBody] UpdateAppointmentCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest("Appointment ID mismatch");

        var result = await _mediator.Send(command, cancellationToken);
        if (result == null)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteAppointment(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteAppointmentCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : NotFound();
    }

    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> GetAppointments(CancellationToken cancellationToken, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 20)
    {
        var query = new GetAppointmentsQuery(new PaginationRequest(pageIndex, pageSize));

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("company/{companyId}")]
    public async Task<IActionResult> GetAppointmentsByCompany([FromRoute] Guid companyId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAppointmentsByCompanyQuery(companyId), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointmentById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetAppointmentByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}