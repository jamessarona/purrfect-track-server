// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        PetOwnerConfiguration
//  Created:     5/17/2025 12:54:12 AM
//
//  This file is part of the PurrfectTack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Infrastructure.Data.Configurations;

public class PetOwnerConfiguration : IEntityTypeConfiguration<PetOwner>
{
    public void Configure(EntityTypeBuilder<PetOwner> builder)
    {
        ContactConfigurationHelper.ConfigureContactFields(builder);

        builder.HasOne(po => po.User)
            .WithOne(u => u.PetOwnerProfile)
            .HasForeignKey<PetOwner>(po => po.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(po => po.Pets)
            .WithOne(p => p.PetOwner)
            .HasForeignKey(p => p.PetOwnerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}