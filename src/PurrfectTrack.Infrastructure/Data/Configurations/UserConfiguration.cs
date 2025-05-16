// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        UserConfiguration
//  Created:     5/17/2025 12:54:12 AM
//
//  This file is part of the PurrfectTack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Infrastructure.Data.Configurations;

class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id); 

        builder.Property(u => u.Email)
            .IsRequired() 
            .HasMaxLength(100); 

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.PasswordHash)
            .IsRequired() 
            .HasMaxLength(200);

        builder.Property(u => u.Role)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(u => u.IsActive)
            .IsRequired();

        builder.HasOne(u => u.PetOwnerProfile)
            .WithOne(po => po.User)
            .HasForeignKey<PetOwner>(po => po.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(u => u.VetProfile)
            .WithOne(v => v.User)
            .HasForeignKey<Vet>(v => v.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(u => u.VetStaffProfile)
            .WithOne(vf => vf.User)
            .HasForeignKey<VetStaff>(vf => vf.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}