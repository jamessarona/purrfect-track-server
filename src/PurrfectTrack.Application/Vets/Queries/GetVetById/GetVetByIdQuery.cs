// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GetVetByIdQuery
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Vets.Queries.GetVetById;

public record GetVetByIdQuery(Guid Id) : IQuery<GetVetByIdResult>;

public record GetVetByIdResult(VetModel Vet);