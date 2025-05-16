// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        DomainException
//  Created:     5/16/2025 6:28:24 PM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Domain;

public class DomainException : Exception
{
    public string ErrorCode { get; }

    public DomainException(string message, string errorCode = "DOMAIN_ERROR")
        : base($"Domain Exception: \"{message}\" thrown from Domain Layer.")
    {
        ErrorCode = errorCode;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Error Code: {ErrorCode}";
    }
}