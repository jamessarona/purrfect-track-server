﻿// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        InternalServerException
//  Created:     5/16/2025 8:04:32 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Shared.Exceptions;

public class InternalServerException : Exception
{
    public InternalServerException(string message) : base(message)
    {
    }

    public InternalServerException(string message, string details)
        : base(message)
    {
        Details = details;
    }

    public string? Details { get; }
}