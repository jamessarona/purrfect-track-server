// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        UploadCompanyImageHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Companies.Commands.UploadCompanyImage;

public class UploadCompanyImageHandler
    : BaseHandler, ICommandHandler<UploadCompanyImageCommand, UploadCompanyImageResult>
{
    private readonly ICacheService _cacheService;
    private readonly IImageStorageService _imageService;

    public UploadCompanyImageHandler(IApplicationDbContext dbContext, ICacheService cacheService, IImageStorageService imageService)
        : base(dbContext)
    {
        _cacheService = cacheService;
        _imageService = imageService;
    }

    public async Task<UploadCompanyImageResult> Handle(UploadCompanyImageCommand command, CancellationToken cancellationToken)
    {
        var company = await dbContext.Companies
            .FirstOrDefaultAsync(c => c.Id == command.CompanyId, cancellationToken);

        if (company == null)
            throw new CompanyNotFoundException(command.CompanyId);

        var imageUrl = await _imageService.SaveImageAsync(command.File, "companies", cancellationToken);
        company.ImageUrl = imageUrl;

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UploadCompanyImageResult(imageUrl);
    }
}