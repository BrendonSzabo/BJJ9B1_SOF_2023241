using Backend.Models;

namespace Backend.Logic
{
    public interface IPlayer
    {
        IQueryable<Player> ReadAll { get; }
        Player ReadById(int id);
        void Create(Player match);
        void Update(Player match);
        void Delete(int id);

    }
}
