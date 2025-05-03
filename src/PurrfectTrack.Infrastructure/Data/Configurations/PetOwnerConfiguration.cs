using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurrfectTrack.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PurrfectTrack.Infrastructure.Data.Configurations;

public class PetOwnerConfiguration : IEntityTypeConfiguration<PetOwner>
{
    public void Configure(EntityTypeBuilder<PetOwner> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasMany(o => o.Pets)
            .WithOne()
            .HasForeignKey(oi => oi.PetOwnerId);

        builder.Property(p => p.FirstName).HasMaxLength(50).IsRequired();
        builder.Property(p => p.LastName).HasMaxLength(50).IsRequired();
        builder.Property(p => p.Email).HasMaxLength(50).IsRequired();
    }
}