
using PurrfectTrack.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PurrfectTrack.Infrastructure.Data.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Title)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.Description)
            .HasMaxLength(250)
            .IsRequired(false);

        builder.Property(a => a.StartDate)
            .IsRequired();

        builder.Property(a => a.EndDate)
            .IsRequired();

        builder.Property(a => a.Location)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(a => a.Notes)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(a => a.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.HasOne(a => a.PetOwner)
            .WithMany()
            .HasForeignKey(a => a.PetOwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Pet)
            .WithMany()
            .HasForeignKey(a => a.PetId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.Vet)
            .WithMany()
            .HasForeignKey(a => a.VetId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.VetStaff)
            .WithMany()
            .HasForeignKey(a => a.VetStaffId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}