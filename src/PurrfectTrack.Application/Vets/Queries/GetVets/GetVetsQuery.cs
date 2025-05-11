using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Vets.Queries.GetVets;

public record GetVetsQuery : IQuery<GetVetsResult>;

public record GetVetsResult(List<VetModel> Vets);