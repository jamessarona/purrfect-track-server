// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        BaseQueryHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Utils;
public abstract class BaseQueryHandler : BaseHandler
{
    protected readonly IMapper mapper;

    protected BaseQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        : base(dbContext)
    {
        this.mapper = mapper;
    }
}