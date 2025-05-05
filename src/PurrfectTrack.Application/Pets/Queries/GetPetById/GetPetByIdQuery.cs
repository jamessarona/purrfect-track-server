using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Pets.Queries.GetPetById;

public record GetPetByIdQuery(Guid PetId) : IQuery<GetPetByIdResult>;

public record GetPetByIdResult(PetModel Pet);
