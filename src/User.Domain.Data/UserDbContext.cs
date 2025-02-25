using Microsoft.EntityFrameworkCore;
using User.Domain;

namespace User.Domain.Data;

public class UserDbContext : DbContext
{
    public static readonly string DefaultSchema = "user";
    
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
    
    public DbSet<Teacher> Teachers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema(DefaultSchema)
            .ApplyConfiguration(new TeacherConfiguration());
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseSnakeCaseNamingConvention();
        if (!optionsBuilder.IsConfigured)
            throw new InvalidOperationException("Context was not configured");
        base.OnConfiguring(optionsBuilder);
    }
}