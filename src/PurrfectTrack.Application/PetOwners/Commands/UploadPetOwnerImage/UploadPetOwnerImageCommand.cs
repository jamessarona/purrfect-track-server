// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        UploadPetOwnerImageCommand
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.PetOwners.Commands.UploadPetOwnerImage;

public record UploadPetOwnerImageCommand(Guid PetOwnerId, IFormFile File) 
    : ICommand<UploadPetOwnerImageResult>;

public record UploadPetOwnerImageResult(string ImageUrl);

public class UploadPetOwnerImageValidator : AbstractValidator<UploadPetOwnerImageCommand>
{
    public UploadPetOwnerImageValidator()
    {
        RuleFor(x => x.PetOwnerId).NotEmpty().WithMessage("PetOwnerId is required");

        RuleFor(x => x.File)
            .NotNull().WithMessage("Image file is required")
            .Must(file => file.Length > 0).WithMessage("Uploaded file is empty")
            .Must(file => file.ContentType.StartsWith("image/")).WithMessage("Only image files are allowed");
    }
}