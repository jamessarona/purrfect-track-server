using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.PetOwners.Queries.GetPetOwners;

public record GetPetOwnersQuery() : IQuery<GetPetOwnersResult>;

public record GetPetOwnersResult(List<PetOwnerModel> PetOwners);