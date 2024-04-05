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

        public void Delete(string id)
        {
            repository.Delete(id);
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public User ReadById(string id)
        {
            var thing = repository.Read(id);
            return repository.ReadAll().FirstOrDefault(x => x.Id == id);
        }

        public User ReadById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(User item)
        {
            repository.Update(item);
        }
    }
}
