// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        CompanyController
//  Created:     5/16/2025 7:32:19 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ISender _mediator;

    public CompanyController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> CreateCompany([FromBody] CreatePetCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetCompanyById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Administrator,Vet,VetStaff")]
    public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] UpdateCompanyCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest("Company ID mismatch");

        var result = await _mediator.Send(command, cancellationToken);

        if (result == null)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteCompany(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteCompanyCommand(id), cancellationToken);

            return result.IsSuccess ? NoContent() : NotFound();
    }

    [HttpGet]
    [Authorize(Roles = "Administrator,PetOwner")]
    public async Task<IActionResult> GetCompanies(CancellationToken cancellatioonToken, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 20)
    {
        var query = new GetCompaniesQuery(new PaginationRequest(pageIndex, pageSize));

        var result = await _mediator.Send(query, cancellatioonToken);

        return Ok(result.Companies);
    }

    public async Task<IActionResult> GetCompanyById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCompanyByIdQuery(id);

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result.Company);
    }
}
