using FluentValidation;
using Microsoft.AspNetCore.Http;
using PurrfectTrack.Shared.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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