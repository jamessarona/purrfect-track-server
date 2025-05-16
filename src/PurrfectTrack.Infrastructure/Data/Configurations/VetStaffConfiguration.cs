namespace PurrfectTrack.Infrastructure.Data.Configurations;

public class VetStaffConfiguration : IEntityTypeConfiguration<VetStaff>
{
    public void Configure(EntityTypeBuilder<VetStaff> builder)
    {
        ContactConfigurationHelper.ConfigureContactFields(builder);

        builder.HasOne(vs => vs.User)
            .WithOne(u => u.VetStaffProfile)
            .HasForeignKey<VetStaff>(vs => vs.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(vs => vs.Position)
            .IsRequired(false)
            .HasMaxLength(50);

        builder.Property(vs => vs.Department)
            .IsRequired(false)
            .HasMaxLength(50);

        builder.Property(vs => vs.EmploymentDate)
            .IsRequired(false);

        builder.HasOne(vs => vs.Company)
            .WithMany(c => c.VetStaffs)
            .HasForeignKey(vs => vs.CompanyId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
