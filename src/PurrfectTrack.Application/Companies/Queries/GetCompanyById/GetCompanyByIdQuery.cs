using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Companies.Queries.GetCompanyById;

public record GetCompanyByIdQuery(Guid Id) : IQuery<GetCompanyByIdResult>;

public record GetCompanyByIdResult(CompanyModel Company);