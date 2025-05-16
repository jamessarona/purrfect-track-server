// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetPetsByOwnerQuery
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Pets.Queries.GetPetsByOwner;

public record GetPetsByOwnerQuery(Guid OwnerId) 
    : IQuery<GetPetsByOwnerResult>;

public record GetPetsByOwnerResult(List<PetModel> Pets);