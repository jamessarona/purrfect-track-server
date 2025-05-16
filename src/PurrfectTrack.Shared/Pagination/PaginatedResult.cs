// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        PaginatedResult
//  Created:     5/16/2025 8:04:32 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Shared.Pagination;

public class PaginatedResult<TEntity>
    (int pageIndex, int PageSize, long count, IEnumerable<TEntity> data)
    where TEntity : class
{
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = PageSize;
    public long Count { get; } = count;
    public IEnumerable<TEntity> Data { get; } = data;
}