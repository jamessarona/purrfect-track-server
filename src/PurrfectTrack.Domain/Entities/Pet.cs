﻿// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        Pet
//  Created:     5/16/2025 6:28:24 PM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Domain.Entities;

public class Pet : Entity<Guid>
{
    [Required]
    public Guid PetOwnerId { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    public string? Species { get; set; }
    public string? Breed { get; set; }
    public string? Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public float? Weight { get; set; }
    public string? Color { get; set; }
    public bool? IsNeutered { get; set; }
    public string? ImageUrl { get; set; }

    public PetOwner PetOwner { get; set; } = default!;

    protected Pet() { }

    public Pet(Guid petOwnerId, string name, string? species = null, string? breed = null, string? gender = null,
        DateTime? dateOfBirth = null, float? weight = null, string? color = null, bool? isNeutered = null)
    {
        Id = Guid.NewGuid();
        PetOwnerId = petOwnerId;
        Name = name;
        Species = species;
        Breed = breed;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        Weight = weight;
        Color = color;
        IsNeutered = isNeutered;
    }
}