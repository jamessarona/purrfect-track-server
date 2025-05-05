using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Pets.Queries.GetPetsByOwner;

public record GetPetsByOwnerQuery(Guid OwnerId) 
    : IQuery<GetPetsByOwnerResult>;

public record GetPetsByOwnerResult(List<PetModel> Pets);