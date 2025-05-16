// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        RefreshTokenConfiguration
//  Created:     5/17/2025 12:54:12 AM
//
//  This file is part of the PurrfectTack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Infrastructure.Data.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(rt => rt.Id);

        builder.Property(rt => rt.Token)
            .IsRequired()  
            .HasMaxLength(500);  

        builder.Property(rt => rt.UserId)
            .IsRequired(); 

        builder.Property(rt => rt.ExpiresAt)
            .IsRequired(false);

        builder.Property(rt => rt.IsRevoked)
            .IsRequired()  
            .HasDefaultValue(false);  

        builder.Property(rt => rt.IpAddress)
            .HasMaxLength(45)  
            .IsRequired(false);

        builder.Property(rt => rt.UserAgent)
            .HasMaxLength(200) 
            .IsRequired(false); 

        builder.HasIndex(rt => rt.Token)
            .IsUnique();

        builder.Property(rt => rt.ReplacedByToken)
            .HasMaxLength(255)
            .IsUnicode(false)
            .IsRequired(false);

        builder.HasOne(rt => rt.User)
            .WithMany()  
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}