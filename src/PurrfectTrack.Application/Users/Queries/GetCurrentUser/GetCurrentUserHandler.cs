// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetCurrentUserHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

using PurrfectTrack.Domain.Entities;

namespace PurrfectTrack.Application.Users.Queries.GetCurrentUser;

public class GetCurrentUserHandler : BaseQueryHandler, IQueryHandler<GetCurrentUserQuery, GetCurrentUserResult>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly Dictionary<UserRole, Func<IQueryable<User>, IQueryable<User>>> _includeMap;
    
    public GetCurrentUserHandler(IApplicationDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        : base(dbContext, mapper)
    {
        _httpContextAccessor = httpContextAccessor;

        _includeMap = new Dictionary<UserRole, Func<IQueryable<User>, IQueryable<User>>>
        {
            [UserRole.Vet] = query => query.Include(u => u.VetProfile).ThenInclude(v => v!.Company),
            [UserRole.VetStaff] = query => query.Include(u => u.VetStaffProfile).ThenInclude(vs => vs!.Company),
            [UserRole.PetOwner] = query => query.Include(u => u.PetOwnerProfile)
        };
    }

    public async Task<GetCurrentUserResult> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = GetCurrentUserIdFromClaims();

        var userRole = await dbContext.Users
            .Where(u => u.Id == currentUserId)
            .Select(u => u.Role)
            .FirstOrDefaultAsync(cancellationToken);

        if (userRole == default)
            throw new UserNotFoundException(currentUserId);

        IQueryable<User> query = dbContext.Users.Where(u => u.Id == currentUserId);

        if (_includeMap.TryGetValue(userRole, out var includeFunc))
        {
            query = includeFunc(query);
        }

        var user = await query.FirstOrDefaultAsync(cancellationToken);

        if (user == null)
            throw new UserNotFoundException(currentUserId);

        var userDetail = mapper.Map<UserDetailModel>(user);

        return new GetCurrentUserResult(userDetail);
    }

    private Guid GetCurrentUserIdFromClaims()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user?.Identity?.IsAuthenticated != true)
            throw new UnauthorizedAccessException("User is not authenticated.");
        var userIdClaim =
            user.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
            user.FindFirst("sub")?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim))
            throw new UnauthorizedAccessException("User ID claim is missing.");

        return Guid.Parse(userIdClaim);
    }
}