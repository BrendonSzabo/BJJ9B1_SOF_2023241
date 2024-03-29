using Backend.Models;

namespace Backend.Logic
{
    public class MatchLogic : IMatch
    {
        IRepository
        public IQueryable<Match> ReadAllPlayers => throw new NotImplementedException();

        public void CreatePlayer(Match player)
        {
            throw new NotImplementedException();
        }

        public void DeletePlayer(int id)
        {
            throw new NotImplementedException();
        }

        public Match ReadPlayerById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePlayer(Match player)
        {
            throw new NotImplementedException();
        }
    }
}
