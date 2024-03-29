using Backend.Models;
using Backend.Repository;

namespace Backend.Logic
{
    public class MatchLogic : IMatch
    {
        IRepository<Match> repository;

        public MatchLogic(IRepository<Match> repository)
        {
            this.repository = repository;
        }

        public IQueryable<Match> ReadAll => repository.ReadAll();

        public void Create(Match item)
        {
            repository.Create(item);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public Match ReadById(int id)
        {
            return repository.Read(id);
        }

        public void Update(Match item)
        {
            repository.Update(item);
        }
    }
}
