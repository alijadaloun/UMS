using Microsoft.AspNetCore.Http;
using Solution1.Domain.Entities;
using Solution1.Persistence.Database;

namespace Solution1.Persistence.Repositories;

public class UserRepository
{
    private readonly UniversityDbContext _universityDbContext;

    public UserRepository(UniversityDbContext universityDbContext)
    {
        _universityDbContext = universityDbContext;
    }

    public async Task addUser(User user)
    {
        await _universityDbContext.Users.AddAsync(user);
        await _universityDbContext.SaveChangesAsync();
        
    }

    public async Task<bool> AddProfilePicture(int id, string password, IFormFile file)
    {
        var u= await _universityDbContext.Users.FindAsync(id);
        if(u==null) throw new KeyNotFoundException("User not found");
        if (u.Password == password)
        {
            u.ProfilePicture = file;
            await _universityDbContext.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }
    }
    public async Task<string> getUser(int id, string password)
    {
        var u = await _universityDbContext.Users.FindAsync(id);
        if (u == null) throw new KeyNotFoundException("User not found");
        
        switch (u.Role)
        {
            case 1:
                return "Student";
            case 2:
                return "Teacher";

            default:
                return "Admin";
            
        }
    }
    public string GetUserRole(int id)
    {
        
        var r =  _universityDbContext.Users.Find(id).Role;
        switch (r)
        {
            case 1:
                return "Student";
            case 2:
                return "Teacher";

            default:
                return "Admin";
            
        }
        
        


    }
    
}