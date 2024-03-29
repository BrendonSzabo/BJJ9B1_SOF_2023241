using Backend.Models;

namespace Backend.Logic
{
    public interface IMatch
    {
        IQueryable<Match> ReadAll { get; }
        Match ReadById(int id);
        void Create(Match player);
        void Update(Match player);
        void Delete(int id);

    }
}
