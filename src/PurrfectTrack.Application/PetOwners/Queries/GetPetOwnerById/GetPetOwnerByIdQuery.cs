using PurrfectTrack.Application.DTOs;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.PetOwners.Queries.GetPetOwnerById;

public record GetPetOwnerByIdQuery(Guid Id) : IQuery<GetPetOwnerByIdResult>;

public record GetPetOwnerByIdResult(PetOwnerModel Pet);