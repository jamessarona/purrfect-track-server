using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurrfectTrack.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PurrfectTrack.Infrastructure.Data.Configurations;

public class PetOwnerConfiguration : IEntityTypeConfiguration<PetOwner>
{
    public void Configure(EntityTypeBuilder<PetOwner> builder)
    {
        builder.HasKey(po => po.Id);

        builder.Property(po => po.FirstName)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(po => po.LastName)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(po => po.Email)
               .HasMaxLength(50)
               .IsRequired();
    }
}