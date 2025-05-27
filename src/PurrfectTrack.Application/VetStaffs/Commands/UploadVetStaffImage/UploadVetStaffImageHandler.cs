// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        UploadVetStaffImageHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.VetStaffs.Commands.UploadVetStaffImage;

public class UploadVetStaffImageHandler
    : BaseHandler, ICommandHandler<UploadVetStaffImageCommand, UploadVetStaffImageResult>
{
    private readonly ICacheService _cacheService;
    private readonly IImageStorageService _imageService;

    public UploadVetStaffImageHandler(IApplicationDbContext dbContext, ICacheService cacheService, IImageStorageService imageService)
        : base(dbContext)
    {
        _cacheService = cacheService;
        _imageService = imageService;
    }

    public async Task<UploadVetStaffImageResult> Handle(UploadVetStaffImageCommand command, CancellationToken cancellationToken)
    {
        var vetStaff = await dbContext.VetStaffs
            .FirstOrDefaultAsync(vs => vs.Id == command.VetStaffId, cancellationToken);

        if (vetStaff == null)
            throw new VetStaffNotFoundException(command.VetStaffId);

        var imageUrl = await _imageService.SaveImageAsync(command.File, "vetStaffs", cancellationToken);
        vetStaff.ImageUrl = imageUrl;

        await dbContext.SaveChangesAsync(cancellationToken);

        await _cacheService.RemoveAsync(CacheKeyManager.GetVetStaffsCacheKey());
        await _cacheService.RemoveAsync(CacheKeyManager.GetVetStaffByIdCacheKey(vetStaff.Id));

        return new UploadVetStaffImageResult(imageUrl);
    }
}