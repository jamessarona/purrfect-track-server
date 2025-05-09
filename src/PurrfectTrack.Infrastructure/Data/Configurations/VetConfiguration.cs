
namespace PurrfectTrack.Infrastructure.Data.Configurations;

public class VetConfiguration : IEntityTypeConfiguration<Vet>
{

    public void Configure(EntityTypeBuilder<Vet> builder)
    {
        builder.HasKey(po => po.Id);

        builder.Property(po => po.FirstName)
               .HasMaxLength(50)
            .IsRequired();

        builder.Property(po => po.LastName)
               .HasMaxLength(50)
            .IsRequired();

        builder.Property(po => po.UserId)
            .IsRequired();

        builder.HasOne(po => po.User)
            .WithOne(u => u.VetProfile)
            .HasForeignKey<Vet>(po => po.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(po => po.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(po => po.Address)
            .HasMaxLength(250);

        builder.Property(p => p.ImageUrl)
            .IsRequired(false)
            .HasMaxLength(500);

        builder.Property(p => p.LicenseNumber)
            .IsRequired(false)
            .HasMaxLength(50);

        builder.Property(p => p.LicenseExpiryDate)
            .IsRequired(false);

        builder.Property(p => p.Specialization)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(p => p.YearsOfExperience)
            .IsRequired(false);

        builder.Property(p => p.ClinicName)
            .IsRequired(false)
            .HasMaxLength(150);

        builder.Property(p => p.ClinicAddress)
            .IsRequired(false)
            .HasMaxLength(300);

        builder.Property(p => p.EmploymentDate)
            .IsRequired(false);
    }
}
