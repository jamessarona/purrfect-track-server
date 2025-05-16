// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        CreatePetCommand
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Pets.Commands.CreatePet;

public record CreatePetCommand(Guid PetOwnerId, string Name, string? Species, string? Breed, string? Gender, DateTime? DateOfBirth, float? Weight, string? Color, bool IsNeutered)
    : ICommand<CreatePetResult>;

public record CreatePetResult(Guid Id);

public class CreatePetCommandValidator : AbstractValidator<CreatePetCommand>
{
    public CreatePetCommandValidator()
    {
        RuleFor(x => x.PetOwnerId).NotNull().WithMessage("Pet Owner is required");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
    }
}