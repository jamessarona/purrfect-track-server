using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Shared.CQRS;
using PurrfectTrack.Shared.Pagination;

namespace PurrfectTrack.Application.Companies.Queries.GetCompanies;

public record GetCompaniesQuery(PaginationRequest PaginationRequest)
    : IQuery<GetCompaniesResult>;

public record GetCompaniesResult(PaginatedResult<CompanyModel> Companies);