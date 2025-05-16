// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        DeleteCompanyHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

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
