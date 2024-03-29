using Backend.Data.Repository;
using Backend.Models;

namespace Backend.Logic
{
    public class UserLogic : IUser
    {
        IRepository<User> repository;

        public UserLogic(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public IQueryable<User> ReadAll => repository.ReadAll();

        public void Create(User item)
        {
            repository.Create(item);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public User ReadById(int id)
        {
            return repository.Read(id);
        }

        public void Update(User item)
        {
            repository.Update(item);
        }
    }
}
