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
}
