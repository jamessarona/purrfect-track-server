// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        IImageStorageService
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Abstractions;

public interface IImageStorageService
{
    Task<string> SaveImageAsync(IFormFile file, string subFolder, CancellationToken cancellationToken = default);
}