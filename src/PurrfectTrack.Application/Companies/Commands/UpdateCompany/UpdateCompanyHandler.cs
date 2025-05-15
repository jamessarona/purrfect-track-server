using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.Exceptions;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Companies.Commands.UpdateCompany;

public class UpdateCompanyHandler
    : BaseHandler, ICommandHandler<UpdateCompanyCommand, UpdateCompanyResult>
{
    public UpdateCompanyHandler(IApplicationDbContext dbContext) 
        : base(dbContext)
    {
    }

    public async Task<UpdateCompanyResult> Handle(UpdateCompanyCommand command, CancellationToken cancellationToken)
    {
        var company = await dbContext.Companies
            .FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);

        if (company is null)
            throw new CompanyNotFoundException(command.Id);

        company.Name = command.Name;
        company.Description = command.Description ?? company.Description;
        company.PhoneNumber = command.PhoneNumber ?? company.PhoneNumber;
        company.Email = command.Email ?? company.Email;
        company.Website = command.Website ?? company.Website;
        company.Address = command.Address ?? company.Address;
        company.TaxpayerId = command.TaxpayerId ?? company.TaxpayerId;
        company.IsActive = command.IsActive;

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateCompanyResult(company.Id);
    }
}