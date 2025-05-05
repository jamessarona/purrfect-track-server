using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Pets.Queries.GetPets;

public record GetPetsQuery()
    : IQuery<GetPetsResult>;

public record GetPetsResult(List<PetModel> Pets);