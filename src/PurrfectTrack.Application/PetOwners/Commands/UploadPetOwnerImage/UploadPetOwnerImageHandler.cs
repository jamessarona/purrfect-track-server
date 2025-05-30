// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        UploadPetOwnerImageHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.PetOwners.Commands.UploadPetOwnerImage;

public class UploadPetOwnerImageHandler
    : BaseHandler, ICommandHandler<UploadPetOwnerImageCommand, UploadPetOwnerImageResult>
{
    private readonly ICacheService _cacheService;
    private readonly IImageStorageService _imageService;

    public UploadPetOwnerImageHandler(IApplicationDbContext dbContext, ICacheService cacheService, IImageStorageService imageService)
        : base(dbContext)
    {
        _cacheService = cacheService;
        _imageService = imageService;
    }

    public async Task<UploadPetOwnerImageResult> Handle(UploadPetOwnerImageCommand command, CancellationToken cancellationToken)
    {
        var petOwner = await dbContext.PetOwners
            .Include(po => po.Pets)
            .FirstOrDefaultAsync(po => po.Id == command.PetOwnerId, cancellationToken);

        if (petOwner == null)
            throw new PetOwnerNotFoundException(command.PetOwnerId);

        var imageUrl = await _imageService.SaveImageAsync(command.File, "pet-owners", cancellationToken);
        petOwner.ImageUrl = imageUrl;

        await dbContext.SaveChangesAsync(cancellationToken);

        await _cacheService.RemoveAsync(CacheKeyManager.GetPetOwnersCacheKey());
        await _cacheService.RemoveAsync(CacheKeyManager.GetPetOwnerByIdCacheKey(petOwner.Id));
        foreach (var pet in petOwner.Pets)
            await _cacheService.RemoveAsync(CacheKeyManager.GetPetOwnerByPetCacheKey(pet.Id));

        return new UploadPetOwnerImageResult(imageUrl);
    }
}