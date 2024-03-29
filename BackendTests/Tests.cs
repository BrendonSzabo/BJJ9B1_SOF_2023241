using AutoFixture;
using AutoFixture.Xunit2;
using Backend.Models;
using Backend.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Backend
{
    public class Tests : IDisposable
    {
        private readonly LeagueDbContext _dbContext;
        private Fixture _fixture;
        public Tests()
        {
            // Initialize your DbContext with an in-memory database for testing
            var options = new DbContextOptionsBuilder<LeagueDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new LeagueDbContext(options);
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void Match_Should_Have_Teams()
        {
            // Arrange
            var match = _fixture.Create<Match>();
            match.Teams.Clear();
            var team1 = _fixture.Create<Team>();
            var team2 = _fixture.Create<Team>();
            match.Teams.Add(team1);
            match.Teams.Add(team2);
            _dbContext.Matches.Add(match);
            _dbContext.SaveChanges();

            // Act
            var savedMatch = _dbContext.Matches.Include(m => m.Teams).FirstOrDefault();

            // Assert
            savedMatch.Should().NotBeNull();
            savedMatch.Teams.Should().HaveCount(2);
        }

        [Fact]
        public void Team_Should_Have_Players()
        {
            // Arrange
            var team = _fixture.Create<Team>();
            team.Players.Clear();
            var player1 = _fixture.Create<Player>();
            var player2 = _fixture.Create<Player>();
            team.Players.Add(player1);
            team.Players.Add(player2);
            _dbContext.Teams.Add(team);
            _dbContext.SaveChanges();

            // Act
            var savedTeam = _dbContext.Teams.Include(t => t.Players).FirstOrDefault();

            // Assert
            savedTeam.Should().NotBeNull();
            savedTeam.Players.Should().HaveCount(2);
        }

        [Fact]
        public void User_Should_Have_Team()
        {
            // Arrange
            var user = _fixture.Create<User>();
            var team = _fixture.Create<Team>();
            user.Team = team;
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            // Act
            var savedUser = _dbContext.Users.Include(u => u.Team).FirstOrDefault();

            // Assert
            savedUser.Should().NotBeNull();
            savedUser.Team.Should().NotBeNull();
        }

        [Fact]
        public void User_Should_Reach_PlayersInTeam()
        {
            // Arrange
            var user = _fixture.Create<User>();
            var team = _fixture.Create<Team>();
            user.Team = team;
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            // Act
            var savedUser = _dbContext.Users.Include(u => u.Team).FirstOrDefault();

            // Assert
            savedUser.Should().NotBeNull();
            savedUser.Team.Should().NotBeNull();
            savedUser.Team.Players.Should().NotBeEmpty();
        }

        public void Dispose()
        {
            // Clean up resources after each test
            _dbContext.Dispose();
        }
    }
}
