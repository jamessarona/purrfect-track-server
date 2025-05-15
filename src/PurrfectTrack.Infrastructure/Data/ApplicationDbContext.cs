using Microsoft.EntityFrameworkCore.Infrastructure;

namespace PurrfectTrack.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly AuditableEntityInterceptor _auditableEntityInterceptor;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, AuditableEntityInterceptor auditableEntityInterceptor)
        : base(options)
    {
        _auditableEntityInterceptor = auditableEntityInterceptor;
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<UserSession> UserSessions => Set<UserSession>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<PetOwner> PetOwners => Set<PetOwner>();
    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<Vet> Vets => Set<Vet>();
    public DbSet<VetStaff> VetStaffs => Set<VetStaff>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Company> Companies => Set<Company>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.AddInterceptors(_auditableEntityInterceptor);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}