namespace PurrfectTrack.Infrastructure.Data.Configurations;

public class VetStaffConfiguration : IEntityTypeConfiguration<VetStaff>
{
    public void Configure(EntityTypeBuilder<VetStaff> builder)
    {
        ContactConfigurationHelper.ConfigureContactFields(builder);

        builder.HasOne(po => po.User)
            .WithOne(u => u.VetStaffProfile)
            .HasForeignKey<VetStaff>(po => po.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.Position)
            .IsRequired(false)
            .HasMaxLength(50);

        builder.Property(p => p.Department)
            .IsRequired(false)
            .HasMaxLength(50);

        builder.Property(p => p.EmploymentDate)
            .IsRequired(false);
    }
}
