using Backend.Models;

namespace Backend.Logic
{
    public interface IUser
    {
        IQueryable<User> ReadAllUsers { get; }
        User ReadUserById(int id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}
