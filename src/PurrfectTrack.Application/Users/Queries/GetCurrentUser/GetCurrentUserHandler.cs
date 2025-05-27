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
    private readonly ICurrentUserService _currentUserService;
    private readonly Dictionary<UserRole, Func<IQueryable<User>, IQueryable<User>>> _includeMap;
    
    public GetCurrentUserHandler(IApplicationDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService)
        : base(dbContext, mapper)
    {
        _currentUserService = currentUserService;

        _includeMap = new Dictionary<UserRole, Func<IQueryable<User>, IQueryable<User>>>
        {
            [UserRole.Vet] = query => query.Include(u => u.VetProfile).ThenInclude(v => v!.Company),
            [UserRole.VetStaff] = query => query.Include(u => u.VetStaffProfile).ThenInclude(vs => vs!.Company),
            [UserRole.PetOwner] = query => query.Include(u => u.PetOwnerProfile)
        };
    }

    public async Task<GetCurrentUserResult> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;

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
}