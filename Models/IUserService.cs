namespace democode.Models
{
    public interface IUserService
    {
        User Register(string id, string name, string email, string password, UserRole role);
        void SaveUser(User user);
        User GetById(string id);
        User GetUser(string email, string password);
        bool IsEmailTaken(string email);
        User Update(string id, string name, string email);
        void DeleteUser(string id);
    }
}
