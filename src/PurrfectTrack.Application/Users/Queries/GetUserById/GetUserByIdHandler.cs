using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Application.Exceptions;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;

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
