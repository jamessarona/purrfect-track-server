using AutoMapper;
using PurrfectTrack.Application.Data;

namespace PurrfectTrack.Application.Utils;
public abstract class BaseQueryHandler : BaseHandler
{
    protected readonly IMapper mapper;

    protected BaseQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        : base(dbContext)
    {
        this.mapper = mapper;
    }
}