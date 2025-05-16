// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        PetConfiguration
//  Created:     5/17/2025 12:54:12 AM
//
//  This file is part of the PurrfectTack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Infrastructure.Data.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
               .HasMaxLength(50)
               .IsRequired();

        builder.HasOne(p => p.PetOwner)
               .WithMany(po => po.Pets)
               .HasForeignKey(p => p.PetOwnerId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.ImageUrl)
            .IsRequired(false)
            .HasMaxLength(500);
    }
}
