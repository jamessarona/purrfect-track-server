using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public static string GetVetByIdCacheKey(Guid id) => $"vets-{id}";
}