using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Companies.Commands.DeleteCompany;

public class DeleteCompanyHandler
    : BaseHandler, ICommandHandler<DeleteCompanyCommand, DeleteCompanyResult>
{
    public DeleteCompanyHandler(IApplicationDbContext dbContext) 
        : base(dbContext)
    {
    }

    public async Task<DeleteCompanyResult> Handle(DeleteCompanyCommand command, CancellationToken cancellationToken)
    {
        var company = await dbContext.Companies
            .FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);

        if (company is null)
            return new DeleteCompanyResult(false);

        dbContext.Companies.Remove(company);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteCompanyResult(true);
    }
}
