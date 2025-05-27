// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        UploadCompanyImageCommand
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Companies.Commands.UploadCompanyImage;

public record UploadCompanyImageCommand(Guid CompanyId, IFormFile File) 
    : ICommand<UploadCompanyImageResult>;

public record UploadCompanyImageResult(string ImageUrl);

public class UploadCompanyImageValidator : AbstractValidator<UploadCompanyImageCommand>
{
    public UploadCompanyImageValidator()
    {
        RuleFor(x => x.CompanyId).NotEmpty().WithMessage("CompanyId is required");

        RuleFor(x => x.File)
            .NotNull().WithMessage("Image file is required")
            .Must(file => file.Length > 0).WithMessage("Uploaded file is empty")
            .Must(file => file.ContentType.StartsWith("image/")).WithMessage("Only image files are allowed");
    }
}