// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetPetOwnerByPetQuery
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.PetOwners.Queries.GetPetOwnerByPet;

public record GetPetOwnerByPetQuery(Guid PetId) : IQuery<GetPetOwnerByPetResult>;

public record GetPetOwnerByPetResult(PetOwnerModel PetOwner);