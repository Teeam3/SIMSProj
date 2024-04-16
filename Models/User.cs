
namespace democode.Models
{
    public enum UserRole
    {
        Admin,
        Teacher,
        Student,
    }

    public class User
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public UserRole Role { get; set; }
        public string? Name { get; set; }
        public string? Course { get; set; }
    }
}

   