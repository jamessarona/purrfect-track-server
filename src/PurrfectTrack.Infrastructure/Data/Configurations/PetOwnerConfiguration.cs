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

        builder.Property(po => po.UserId)
            .IsRequired();

        builder.HasOne(po => po.User)
            .WithOne(u => u.PetOwnerProfile)
            .HasForeignKey<PetOwner>(po => po.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(po => po.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(po => po.Address)
            .HasMaxLength(250);

        builder.Property(p => p.ImageUrl)
            .IsRequired(false)
            .HasMaxLength(500);
    }
}