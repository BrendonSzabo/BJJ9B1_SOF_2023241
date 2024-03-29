using Backend.Models;

namespace Backend.Logic
{
    public interface IMatch
    {
        IQueryable<Match> ReadAllPlayers { get; }
        Match ReadPlayerById(int id);
        void CreatePlayer(Match player);
        void UpdatePlayer(Match player);
        void DeletePlayer(int id);

    }
}
