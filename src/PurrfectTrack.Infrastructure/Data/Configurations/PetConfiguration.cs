using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurrfectTrack.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PurrfectTrack.Infrastructure.Data.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.HasKey(o => o.Id);

        builder.HasOne<Pet>()
                .WithMany()
                .HasForeignKey(oi => oi.PetOwnerId);
        builder.Property(oi => oi.Name).HasMaxLength(50).IsRequired();
    }
}
