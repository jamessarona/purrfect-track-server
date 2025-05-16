// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetPetsQuery
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Pets.Queries.GetPets;

public record GetPetsQuery()
    : IQuery<GetPetsResult>;

public record GetPetsResult(List<PetModel> Pets);