namespace PurrfectTrack.Infrastructure.Data.Configurations;

class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id); 

        builder.Property(u => u.Email)
            .IsRequired() 
            .HasMaxLength(100); 

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.PasswordHash)
            .IsRequired() 
            .HasMaxLength(200);

        builder.Property(u => u.Role)
            .IsRequired() 
            .HasMaxLength(50);

        builder.Property(u => u.IsActive)
            .IsRequired(); 

        builder.HasOne(u => u.PetOwnerProfile)
            .WithOne(po => po.User)
            .HasForeignKey<PetOwner>(po => po.UserId) 
            .OnDelete(DeleteBehavior.Cascade); 
    }
}