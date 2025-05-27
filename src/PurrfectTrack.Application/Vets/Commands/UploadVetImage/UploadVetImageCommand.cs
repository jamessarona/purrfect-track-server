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

namespace PurrfectTrack.Application.Vets.Commands.UploadVetImage;

public record UploadVetImageCommand(Guid VetId, IFormFile File) 
    : ICommand<UploadVetImageResult>;

public record UploadVetImageResult(string ImageUrl);

public class UploadVetImageValidator : AbstractValidator<UploadVetImageCommand>
{
    public UploadVetImageValidator()
    {
        RuleFor(x => x.VetId).NotEmpty().WithMessage("VetId is required");

        RuleFor(x => x.File)
            .NotNull().WithMessage("Image file is required")
            .Must(file => file.Length > 0).WithMessage("Uploaded file is empty")
            .Must(file => file.ContentType.StartsWith("image/")).WithMessage("Only image files are allowed");
    }
}