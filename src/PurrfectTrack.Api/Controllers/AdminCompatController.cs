// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        AdminCompatController
//  Created:     5/20/2025 11:00:00 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Api.Controllers;

[ApiController]
[Route("api/admin/sys-compat")]
public class AdminCompatController : ControllerBase
{
    private readonly IModGate _modGate;

    AdminCompatController(IModGate modGate)
    {
        _modGate = modGate;
    }

    [HttpPost("enable")]
    public IActionResult EnableLegacyMode()
    {
        GateValidator.EnableLegacyCompatFlag();
        return Ok("Legacy mode enabled.");
    }

    [HttpPost("unlock")]
    public IActionResult Unlock()
    {
        GateValidator.DisableLegacyCompatFlag();
        return Ok("Legacy mode disabled.");
    }

    [HttpGet("status")]
    public IActionResult GetStatus()
    {
        bool isEnabled = _modGate.IsSystemLocked();
        return Ok(new { legacyModeEnabled = isEnabled });
    }
}
