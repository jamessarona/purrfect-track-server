using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Application.Exceptions;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;

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