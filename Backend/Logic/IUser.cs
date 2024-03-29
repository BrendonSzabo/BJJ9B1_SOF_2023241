using Backend.Models;

namespace Backend.Logic
{
    public interface IUser
    {
        IQueryable<User> ReadAll { get; }
        User ReadById(int id);
        void Create(User user);
        void Update(User user);
        void Delete(int id);
    }
}
