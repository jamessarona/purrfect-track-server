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
        var currentUserId = GetCurrentUserId();

        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == currentUserId, cancellationToken);

        if (user == null)
            throw new UserNotFoundException(currentUserId);

        var userModel = mapper.Map<UserModel>(user);

        return new GetCurrentUserResult(userModel);
    }

    private Guid GetCurrentUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            throw new UnauthorizedAccessException("User is not authenticated.");

        return Guid.Parse(userIdClaim);
    }
}