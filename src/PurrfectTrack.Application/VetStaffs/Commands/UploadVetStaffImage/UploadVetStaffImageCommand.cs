// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        UploadVetImageCommand
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.VetStaffs.Commands.UploadVetStaffImage;

public record UploadVetStaffImageCommand(Guid VetStaffId, IFormFile File) 
    : ICommand<UploadVetStaffImageResult>;

public record UploadVetStaffImageResult(string ImageUrl);

public class UploadVetStaffImageValidator : AbstractValidator<UploadVetStaffImageCommand>
{
    public UploadVetStaffImageValidator()
    {
        RuleFor(x => x.VetStaffId).NotEmpty().WithMessage("VetStaffId is required");

        RuleFor(x => x.File)
            .NotNull().WithMessage("Image file is required")
            .Must(file => file.Length > 0).WithMessage("Uploaded file is empty")
            .Must(file => file.ContentType.StartsWith("image/")).WithMessage("Only image files are allowed");
    }
}