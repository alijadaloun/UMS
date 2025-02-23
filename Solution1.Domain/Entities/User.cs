using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Solution1.Domain.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    [NotMapped]
    public IFormFile ProfilePicture { get; set; } = null;//  cant store iformfile in db
    public int Role { get; set; } = 0;
    //1:Student
    //2:Teacher
    //3:Admin
}