
namespace PurrfectTrack.Infrastructure.Data.Configurations;

public class VetConfiguration : IEntityTypeConfiguration<Vet>
{

    public void Configure(EntityTypeBuilder<Vet> builder)
    {
        ContactConfigurationHelper.ConfigureContactFields(builder);

        builder.HasOne(v => v.User)
            .WithOne(u => u.VetProfile)
            .HasForeignKey<Vet>(v => v.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(v => v.LicenseNumber)
            .IsRequired(false)
            .HasMaxLength(50);

        builder.Property(v => v.LicenseExpiryDate)
            .IsRequired(false);

        builder.Property(v => v.Specialization)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(v => v.YearsOfExperience)
            .IsRequired(false);

        builder.Property(v => v.ClinicName)
            .IsRequired(false)
            .HasMaxLength(150);

        builder.Property(v => v.ClinicAddress)
            .IsRequired(false)
            .HasMaxLength(300);

        builder.Property(v => v.EmploymentDate)
            .IsRequired(false);

        builder.HasOne(v => v.Company)
            .WithMany(c => c.Vets)
            .HasForeignKey(v => v.CompanyId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
