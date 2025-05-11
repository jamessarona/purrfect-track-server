namespace PurrfectTrack.Infrastructure.Data.Configurations;

public static class ContactConfigurationHelper
{
    public static void ConfigureContactFields<T>(EntityTypeBuilder<T> builder) where T : Contact
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

        builder.Property(po => po.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(po => po.Address)
            .HasMaxLength(250);

        builder.Property(p => p.ImageUrl)
            .IsRequired(false)
            .HasMaxLength(500);
    }
}
