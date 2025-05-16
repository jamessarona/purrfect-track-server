// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        UploadPetImageCommand
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Pets.Commands.UploadPetImage;

public record UploadPetImageCommand(Guid PetId, IFormFile File)
    : ICommand<UploadPetImageResult>;

public record UploadPetImageResult(string ImageUrl);

public class UploadPetImageValidator : AbstractValidator<UploadPetImageCommand>
{
    public UploadPetImageValidator()
    {
        RuleFor(x => x.PetId).NotEmpty().WithMessage("PetId is required");

        RuleFor(x => x.File)
            .NotNull().WithMessage("Image file is required")
            .Must(file => file.Length > 0).WithMessage("Uploaded file is empty")
            .Must(file => file.ContentType.StartsWith("image/")).WithMessage("Only image files are allowed");
    }
}