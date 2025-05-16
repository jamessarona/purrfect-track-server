// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        CreateCompanyHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Companies.Commands.CreateCompany;

public class CreateCompanyHandler
    : BaseHandler, ICommandHandler<CreateCompanyCommand, CreateCompanyResult>
{
    public CreateCompanyHandler(IApplicationDbContext dbContext) 
        : base(dbContext) { }

    public async Task<CreateCompanyResult> Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        var company = new Company(
            command.Name,
            command.Description,
            command.PhoneNumber,
            command.Email,
            command.Website,
            command.Address,
            command.TaxpayerId,
            command.IsActive
           );

        dbContext.Companies.Add(company);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateCompanyResult(company.Id);
    }
}
