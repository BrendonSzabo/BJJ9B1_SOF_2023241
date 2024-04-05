using AutoFixture;
using AutoFixture.AutoMoq;
using Backend.Data;
using Backend.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DatabaseTests
{
    public class RelationTests
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Fixture _fixture;
        public RelationTests()
        {
            // Initialize your DbContext with an in-memory database for testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new ApplicationDbContext(options);

            _dbContext.Database.EnsureCreated();
            _dbContext.SeedData();

            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _fixture.Customize<User>(o => o.Without(u => u.Team));
            _fixture.Customize<Match>(o => o.Without(u => u.Teams));
            _fixture.Customize<Team>(o => o.Without(u => u.Matches));
            _fixture.Customize<Team>(o => o.Without(u => u.User));
            _fixture.Customize<Player>(o => o.Without(u => u.Team));
        }

        [Fact]
        public void Match_Should_Have_Teams()
        {
            
            // Arrange

            // Act
            var team = _dbContext.Teams.FirstOrDefault();

            // Assert
            team.Should().NotBeNull();
            team.Id.Should().BeGreaterThan(0);
            team.User.Should().NotBeNull();
            team.User.Id.Should().Be(team.UserId);
            team.Matches.Should().NotBeNull();
            team.Players.Should().NotBeNull();
        }
        [Fact]
        public void CRUD_Player_Should_Work()
        {

            // Arrange
            var player = new Player(){Name = "Test Player", Image = "asd", Language = "asd", Nationality = "asd"};
            // Act
            _dbContext.Players.Add(player);
            _dbContext.SaveChanges();

            // Assert
            _dbContext.Players.Should().Contain(x => x.Name == player.Name);

            
        }
    }
}