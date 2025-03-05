using EnrollmentMS.Domain;
using Microsoft.EntityFrameworkCore;

namespace EnrollmentMS.Persistence;

public class EnrollmentDbContext: DbContext
{
    public DbSet<Enrollment> Enrollments { get; set; }

    public EnrollmentDbContext(DbContextOptions<EnrollmentDbContext> options): base(options)
    {
        
        
    }
    
    
    
}