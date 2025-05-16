// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        CacheService
//  Created:     5/17/2025 12:54:12 AM
//
//  This file is part of the PurrfectTack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Infrastructure.Services;

public class CacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;

    public CacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task SetAsync(string key, object value, TimeSpan expiration)
    {
        _memoryCache.Set(key, value, expiration);
        await Task.CompletedTask;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        _memoryCache.TryGetValue(key, out T? value);
        return await Task.FromResult(value);
    }

    public async Task RemoveAsync(string key)
    {
        _memoryCache.Remove(key);
        await Task.CompletedTask;
    }
}
