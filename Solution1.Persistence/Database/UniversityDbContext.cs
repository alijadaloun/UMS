using Microsoft.EntityFrameworkCore;
using Solution1.Domain.Entities;

namespace Solution1.Persistent.Database;

public class UniversityDbContext: DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    
    public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options)
    {
        //dotnet ef migrations add "MigrationName " --context DbContext --project Persistence --startup-project Presentation --verbose -o Migrations
    }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        
        modelBuilder.HasDefaultSchema("public");

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=university;Username=ALIJAD;Password=alijad");

//dotnet tool install --global dotnet-ef
//    dotnet tool update --global dotnet-ef

    
    
    
}