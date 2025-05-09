namespace PurrfectTrack.Infrastructure.Data.Configurations;

public class VetStaffConfiguration : IEntityTypeConfiguration<VetStaff>
{
    public void Configure(EntityTypeBuilder<VetStaff> builder)
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
            .WithOne(u => u.VetStaffProfile)
            .HasForeignKey<VetStaff>(po => po.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(po => po.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(po => po.Address)
            .HasMaxLength(250);

        builder.Property(p => p.ImageUrl)
            .IsRequired(false)
            .HasMaxLength(500);

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
