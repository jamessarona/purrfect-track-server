using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.VetStaffs.Queries.GetVets;

public record GetVetStaffsQuery : IQuery<GetVetStaffsResult>;

public record GetVetStaffsResult(List<VetStaffModel> VetStaffs);