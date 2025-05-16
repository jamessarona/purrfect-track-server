// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        IQuery
//  Created:     5/16/2025 8:04:32 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

using MediatR;

namespace PurrfectTrack.Shared.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>
        where TResponse : notnull
{
}