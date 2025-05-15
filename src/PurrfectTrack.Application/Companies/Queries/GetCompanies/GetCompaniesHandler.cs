using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;
using PurrfectTrack.Shared.Pagination;

namespace PurrfectTrack.Application.Companies.Queries.GetCompanies;

public class GetCompaniesHandler
    : BaseQueryHandler, IQueryHandler<GetCompaniesQuery, GetCompaniesResult>
{
    public GetCompaniesHandler(IApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public async Task<GetCompaniesResult> Handle(GetCompaniesQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var companyQuery = dbContext.Companies.AsQueryable();

        var totalCount = await companyQuery.CountAsync(cancellationToken);

        var companies = await companyQuery
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var companiesModels = mapper.Map<List<CompanyModel>>(companies);

        var paginatedResult = new PaginatedResult<CompanyModel>(
            pageIndex,
            pageSize,
            totalCount,
            companiesModels
        );

        return new GetCompaniesResult(paginatedResult);
    }
}
