using Backend.Data.Repository;
using Backend.Models;

namespace Backend.Logic
{
    public class PlayerLogic : IPlayer
    {
        IRepository<Player> repository;

        public PlayerLogic(IRepository<Player> repository)
        {
            this.repository = repository;
        }

        public IQueryable<Player> ReadAll => repository.ReadAll();

        public void Create(Player item)
        {
            repository.Create(item);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public Player ReadById(int id)
        {
            return repository.Read(id);
        }

        public void Update(Player item)
        {
            repository.Update(item);
        }
    }
}
