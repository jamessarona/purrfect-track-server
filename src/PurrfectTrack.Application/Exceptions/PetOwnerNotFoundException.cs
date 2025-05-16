// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        PetOwnerNotFoundException
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Exceptions;

class PetOwnerNotFoundException : NotFoundException
{
    public PetOwnerNotFoundException(Guid id) : base("Pet Owner", id)
    {
    }
}
