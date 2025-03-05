using Microsoft.EntityFrameworkCore;
using Solution1.Domain.Entities;

namespace Solution1.Persistence.Database;

public class UniversityDbContext: DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<User> Users { get; set; }
    
    public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options)
    {
        //dotnet ef migrations add "MigrationName " --context DbContext --project Persistence --startup-project Presentation --verbose -o Migrations
    }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .HasMany(s => s.Courses).WithMany(c =>c.Students);
        modelBuilder.Entity<Course>().HasMany(s => s.Students).WithMany(s => s.Courses);

        modelBuilder.Entity<Teacher>().HasMany(t=>t.Courses).WithMany(c=>c.Teachers);
        modelBuilder.Entity<Class>().HasMany(c=>c.Courses).WithOne(c => c.Class).HasForeignKey(c=>c.classId);
        modelBuilder.HasDefaultSchema("public");
        

    }
   
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=unidb;Username=ALIJAD;Password=alijad");

    
    
    
}