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

using System.Security.Claims;

namespace PurrfectTrack.Application.Users.Queries.GetCurrentUser;

public class GetCurrentUserHandler : BaseQueryHandler, IQueryHandler<GetCurrentUserQuery, GetCurrentUserResult>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetCurrentUserHandler(IApplicationDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        : base(dbContext, mapper)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GetCurrentUserResult> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = GetCurrentUserIdFromClaims();

        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == currentUserId, cancellationToken);

        if (user == null)
            throw new UserNotFoundException(currentUserId);

        var userModel = mapper.Map<UserModel>(user);

        return new GetCurrentUserResult(userModel);
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