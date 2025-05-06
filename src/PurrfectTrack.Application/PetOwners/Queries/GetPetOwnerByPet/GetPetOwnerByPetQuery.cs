using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.PetOwners.Queries.GetPetOwnerByPet;

public record GetPetOwnerByPetQuery(Guid PetId) : IQuery<GetPetOwnerByPetResult>;

public record GetPetOwnerByPetResult(PetOwnerModel PetOwner);