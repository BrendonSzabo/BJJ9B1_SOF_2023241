using AutoFixture;
using AutoFixture.AutoMoq;
using Backend.Data;
using Backend.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DatabaseTests
{
    public class RelationTests : IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        public RelationTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new ApplicationDbContext(options);

            _dbContext.Database.EnsureCreated();
            _dbContext.SeedData();
        }
        [Fact]
        public void TestStuff_ItWontRunSequentiallyOtherwise_HorribleSolutionIDGAF()
        {
            Match_Should_Have_Teams();
            CRUD_Player_Should_Work();
        }

        private void Match_Should_Have_Teams()
        {
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

        private void CRUD_Player_Should_Work()
        {

            // Arrange
            var player = new Player(){Name = "Test Player", Image = "asd", Language = "asd", Nationality = "asd"};

            // Act
            _dbContext.Players.Add(player);
            _dbContext.SaveChanges();

            // Assert
            _dbContext.Players.Should().Contain(x => x.Name == player.Name);

            
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}