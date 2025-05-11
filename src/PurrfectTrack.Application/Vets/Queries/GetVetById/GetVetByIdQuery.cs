using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Vets.Queries.GetVetById;

public record GetVetByIdQuery(Guid Id) : IQuery<GetVetByIdResult>;

public record GetVetByIdResult(VetModel Vet);