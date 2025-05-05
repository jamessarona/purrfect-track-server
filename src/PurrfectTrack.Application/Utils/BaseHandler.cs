using PurrfectTrack.Application.Data;

namespace PurrfectTrack.Application.Utils;

public abstract class BaseHandler
{
    protected readonly IApplicationDbContext dbContext;

    protected BaseHandler(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
}