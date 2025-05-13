namespace PurrfectTrack.Infrastructure.Data.Configurations;

public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
{
    public void Configure(EntityTypeBuilder<UserSession> builder)
    {
        builder.HasKey(us => us.Id);

        builder.Property(us => us.Token)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(us => us.IpAddress)
            .HasMaxLength(45);

        builder.Property(us => us.UserAgent)
            .HasMaxLength(512);

        builder.HasOne(us => us.User)
            .WithMany()
            .HasForeignKey(us => us.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
