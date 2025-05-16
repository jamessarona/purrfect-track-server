// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        UploadPetImageHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Pets.Commands.UploadPetImage;

public class UploadPetImageHandler
    : BaseHandler, ICommandHandler<UploadPetImageCommand, UploadPetImageResult>
{
    private readonly ICacheService _cacheService;
    private readonly IImageStorageService _imageService;

    public UploadPetImageHandler(IApplicationDbContext dbContext, ICacheService cacheService, IImageStorageService imageService)
        : base(dbContext)
    {
        _cacheService = cacheService;
        _imageService = imageService;
    }

    public async Task<UploadPetImageResult> Handle(UploadPetImageCommand command, CancellationToken cancellationToken)
    {
        var pet = await dbContext.Pets
            .FirstOrDefaultAsync(p => p.Id == command.PetId, cancellationToken);

        if (pet == null)
            throw new PetNotFoundException(command.PetId);

        var imageUrl = await _imageService.SaveImageAsync(command.File, "pet", cancellationToken);
        pet.ImageUrl = imageUrl;

        await dbContext.SaveChangesAsync(cancellationToken);

        await _cacheService.RemoveAsync(CacheKeyManager.GetPetsCacheKey());
        await _cacheService.RemoveAsync(CacheKeyManager.GetPetByIdCacheKey(pet.Id));
        await _cacheService.RemoveAsync(CacheKeyManager.GetPetsByOwner(pet.PetOwnerId));

        await _cacheService.RemoveAsync(CacheKeyManager.GetPetOwnersCacheKey());
        await _cacheService.RemoveAsync(CacheKeyManager.GetPetOwnerByIdCacheKey(pet.PetOwnerId));
        await _cacheService.RemoveAsync(CacheKeyManager.GetPetOwnerByPetCacheKey(pet.Id));

        return new UploadPetImageResult(imageUrl);
    }
}
