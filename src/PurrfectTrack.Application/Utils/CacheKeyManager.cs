// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        CacheKeyManager
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Infrastructure.Caching;

public static class CacheKeyManager
{
    public static string GetPetOwnersCacheKey() => "petOwners";
    public static string GetPetOwnerByIdCacheKey(Guid id) => $"petOwner-{id}";
    public static string GetPetOwnerByPetCacheKey(Guid petId) => $"petOwnerByPet-{petId}";

    public static string GetPetsCacheKey() => "pets";
    public static string GetPetByIdCacheKey(Guid id) => $"pet-{id}";
    public static string GetPetsByOwner(Guid ownerId) => $"petsByPetOwner-{ownerId}";

    public static string GetVetsCacheKey() => "vets";
    public static string GetVetByIdCacheKey(Guid id) => $"vet-{id}";

    public static string GetVetStaffsCacheKey() => "vetStaffs";
    public static string GetVetStaffByIdCacheKey(Guid id) => $"vetStaff-{id}";
}