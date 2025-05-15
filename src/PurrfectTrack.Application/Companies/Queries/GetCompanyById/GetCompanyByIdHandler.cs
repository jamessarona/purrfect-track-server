using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Application.Exceptions;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Companies.Queries.GetCompanyById;

public class GetCompanyByIdHandler
    : BaseQueryHandler, IQueryHandler<GetCompanyByIdQuery, GetCompanyByIdResult>
{
    public GetCompanyByIdHandler(IApplicationDbContext dbContext, IMapper mapper) 
        : base(dbContext, mapper)
    {
    }

    public async Task<GetCompanyByIdResult> Handle(GetCompanyByIdQuery query, CancellationToken cancellationToken)
    {
        var company = await dbContext.Companies
            .FirstOrDefaultAsync(c => c.Id == query.Id, cancellationToken);

        if (company is null)
            throw new CompanyNotFoundException(query.Id);

        var companyModel = mapper.Map<CompanyModel>(company);

        return new GetCompanyByIdResult(companyModel);
    }
}