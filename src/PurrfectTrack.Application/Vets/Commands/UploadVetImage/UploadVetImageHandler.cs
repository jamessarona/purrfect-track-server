// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        UploadVetImageHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Vets.Commands.UploadVetImage;

public class UploadVetStaffImageHandler
    : BaseHandler, ICommandHandler<UploadVetImageCommand, UploadVetImageResult>
{
    private readonly ICacheService _cacheService;
    private readonly IImageStorageService _imageService;

    public UploadVetStaffImageHandler(IApplicationDbContext dbContext, ICacheService cacheService, IImageStorageService imageService)
        : base(dbContext)
    {
        _cacheService = cacheService;
        _imageService = imageService;
    }

    public async Task<UploadVetImageResult> Handle(UploadVetImageCommand command, CancellationToken cancellationToken)
    {
        var vet = await dbContext.Vets
            .FirstOrDefaultAsync(v => v.Id == command.VetId, cancellationToken);

        if (vet == null)
            throw new VetNotFoundException(command.VetId);

        var imageUrl = await _imageService.SaveImageAsync(command.File, "vets", cancellationToken);
        vet.ImageUrl = imageUrl;

        await dbContext.SaveChangesAsync(cancellationToken);

        await _cacheService.RemoveAsync(CacheKeyManager.GetVetsCacheKey());
        await _cacheService.RemoveAsync(CacheKeyManager.GetVetByIdCacheKey(vet.Id));

        return new UploadVetImageResult(imageUrl);
    }
}