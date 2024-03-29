using Backend.Models;

namespace Backend.Logic
{
    public interface ITeam
    {
        IQueryable<Team> ReadAllTeams { get; }
        Team ReadTeamById(int id);
        void CreateTeam(Team team);
        void UpdateTeam(Team team);
        void DeleteTeam(int id);

    }
}
