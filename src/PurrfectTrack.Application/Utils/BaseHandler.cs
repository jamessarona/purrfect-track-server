// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        BaseHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Utils;

public abstract class BaseHandler
{
    protected readonly IApplicationDbContext dbContext;

    protected BaseHandler(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
}