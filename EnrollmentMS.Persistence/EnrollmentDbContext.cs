using EnrollmentMS.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EnrollmentMS.Persistence;

public class EnrollmentDbContext: DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public DbSet<Enrollment> Enrollments { get; set; }
    public EnrollmentDbContext(DbContextOptions<EnrollmentDbContext> options, IHttpContextAccessor httpContextAccessor ): base(options)
    {
        _httpContextAccessor = httpContextAccessor;
        
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var tenant = _httpContextAccessor.HttpContext?.Items["Tenant"]?.ToString();

        if (!string.IsNullOrEmpty(tenant))
        {
            modelBuilder.HasDefaultSchema(tenant);
        }

        base.OnModelCreating(modelBuilder);
    }
    
    
    
}