// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetPetOwnersQuery
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.PetOwners.Queries.GetPetOwners;

public record GetPetOwnersQuery() : IQuery<GetPetOwnersResult>;

public record GetPetOwnersResult(List<PetOwnerModel> PetOwners);