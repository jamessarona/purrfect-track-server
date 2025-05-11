using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.VetStaffs.Queries.GetVetById;

public record GetVetStaffByIdQuery(Guid Id) : IQuery<GetVetStaffByIdResult>;

public record GetVetStaffByIdResult(VetStaffModel VetStaff);