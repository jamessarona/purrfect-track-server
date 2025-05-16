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
            .HasConversion<string>()
            .IsRequired();

        builder.Property(u => u.IsActive)
            .IsRequired();

        builder.HasOne(u => u.PetOwnerProfile)
            .WithOne(po => po.User)
            .HasForeignKey<PetOwner>(po => po.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(u => u.VetProfile)
            .WithOne(v => v.User)
            .HasForeignKey<Vet>(v => v.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(u => u.VetStaffProfile)
            .WithOne(vf => vf.User)
            .HasForeignKey<VetStaff>(vf => vf.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}