using Backend.Models;

namespace Backend.Logic
{
    public interface IPlayer
    {
        IQueryable<Player> ReadAllPlayers { get; }
        Player ReadMatchById(int id);
        void CreateMatch(Player match);
        void UpdateMatch(Player match);
        void DeleteMatch(int id);

    }
}
