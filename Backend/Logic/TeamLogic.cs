using Backend.Models;
using Backend.Repository;

namespace Backend.Logic
{
    public class TeamLogic : ITeam
    {
        IRepository<Team> repository;

        public TeamLogic(IRepository<Team> repository)
        {
            this.repository = repository;
        }

        public IQueryable<Team> ReadAll => repository.ReadAll();

        public void Create(Team item)
        {
            repository.Create(item);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public Team ReadById(int id)
        {
            return repository.Read(id);
        }

        public void Update(Team item)
        {
            repository.Update(item);
        }
    }
}
