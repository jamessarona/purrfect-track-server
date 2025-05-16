// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetUserByIdHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Users.Queries.GetUserById;

public class GetUserByIdHandler
    : BaseQueryHandler, IQueryHandler<GetUserByIdQuery, GetUserByIdResult>
{
    public GetUserByIdHandler(IApplicationDbContext dbContext, IMapper mapper) 
        : base(dbContext, mapper) { }

    public async Task<GetUserByIdResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

        if (user == null)
            throw new UserNotFoundException(request.Id);

        var userModel = mapper.Map<UserModel>(user);

        return new GetUserByIdResult(userModel);
    }
}
