using Backend.Models;

namespace Backend.Logic
{
    public interface ITeam
    {
        IQueryable<Team> ReadAll { get; }
        Team ReadById(int id);
        void Create(Team team);
        void Update(Team team);
        void Delete(int id);

    }
}
