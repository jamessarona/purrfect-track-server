using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;

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
