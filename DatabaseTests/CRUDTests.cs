using Backend.Data.Repository;
using Backend.Logic;
using Backend.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTests
{
    public class CRUDTests
    {
        PlayerLogic Plogic;
        Mock<IRepository<Player>> mockPlayers;
        TeamLogic Tlogic;
        Mock<IRepository<Team>> mockTeams;
        MatchLogic Mlogic;
        Mock<IRepository<Backend.Models.Match>> mockMatches;
        UserLogic ULogic;
        Mock<IRepository<User>> mockUsers;

        public CRUDTests()
        {
            mockPlayers = new Mock<IRepository<Player>>();
            mockPlayers.Setup(m => m.ReadAll()).Returns(new List<Player>()
            {
                new Player{Name = "Test Entity", Image = "asd", Language = "asd", Nationality = "asd" }

            }.AsQueryable());
            mockPlayers.Setup(m => m.Read(It.IsAny<int>())).Returns(new Player() { Name = "Test Entity", Image = "asd", Language = "asd", Nationality = "asd" });
            Plogic = new PlayerLogic(mockPlayers.Object);

            mockTeams = new Mock<IRepository<Team>>();
            mockTeams.Setup(m => m.ReadAll()).Returns(new List<Team>()
            {
                new Team{Name = "Test Entity" }

            }.AsQueryable());
            mockTeams.Setup(m => m.Read(It.IsAny<int>())).Returns(new Team() { Name = "Test Entity" });
            Tlogic = new TeamLogic(mockTeams.Object);

            mockMatches = new Mock<IRepository<Backend.Models.Match>>();
            mockMatches.Setup(m => m.ReadAll()).Returns(new List<Backend.Models.Match>()
            {
                new Backend.Models.Match{Date = "1231.11.12" }

            }.AsQueryable());
            mockMatches.Setup(m => m.Read(It.IsAny<int>())).Returns(new Backend.Models.Match() { Date = "1231.11.12", Teams = new List<Team>() });
            Mlogic = new MatchLogic(mockMatches.Object);

            mockUsers = new Mock<IRepository<User>>();
            mockUsers.Setup(m => m.ReadAll()).Returns(new List<User>()
            {
                new User{UserName = "Test Entity", Email = "asd"}

            }.AsQueryable());
            mockUsers.Setup(m => m.Read(It.IsAny<int>())).Returns(new User() { UserName = "Test Entity", Email = "asd"});
            ULogic = new UserLogic(mockUsers.Object);
        }

        [Fact]
        public void CRUD_Player_Should_Work()
        {
            // Arrange
            var originalEntity = new Player() { Name = "Test Entity", Image = "asd", Language = "asd", Nationality = "asd" };

            // Act
            var action = new Action(() => Plogic.Create(originalEntity));

            // Assert
            action.Should().NotThrow();
            mockPlayers.Verify(m => m.Create(It.IsAny<Player>()), Times.Once);

            var entities = Plogic.ReadAll.ToList();
            entities.Should().NotBeNull();
            entities.Should().HaveCount(1);
            entities.First().Name.Should().Be("Test Entity");

            var entity = Plogic.ReadById(originalEntity.Id);
            entity.Should().NotBeNull();
            entity.Name.Should().Be("Test Entity");

            originalEntity.Name = "Updated Entity";
            action = new Action(() => Plogic.Update(originalEntity));
            action.Should().NotThrow();
            mockPlayers.Verify(m => m.Update(It.IsAny<Player>()), Times.Once);

            action = new Action(() => Plogic.Delete(originalEntity.Id));
            action.Should().NotThrow();
            mockPlayers.Verify(m => m.Delete(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void CRUD_Team_Should_Work()
        {
            // Arrange
            var originalEntity = new Team() { Name = "Test Entity" };

            // Act
            var action = new Action(() => Tlogic.Create(originalEntity));

            // Assert
            action.Should().NotThrow();
            mockTeams.Verify(m => m.Create(It.IsAny<Team>()), Times.Once);

            var entities = Plogic.ReadAll.ToList();
            entities.Should().NotBeNull();
            entities.Should().HaveCount(1);
            entities.First().Name.Should().Be("Test Entity");

            var entity = Plogic.ReadById(originalEntity.Id);
            entity.Should().NotBeNull();
            entity.Name.Should().Be("Test Entity");

            originalEntity.Name = "Updated Entity";
            action = new Action(() => Tlogic.Update(originalEntity));
            action.Should().NotThrow();
            mockTeams.Verify(m => m.Update(It.IsAny<Team>()), Times.Once);

            action = new Action(() => Tlogic.Delete(originalEntity.Id));
            action.Should().NotThrow();
            mockTeams.Verify(m => m.Delete(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void CRUD_Match_Should_Work()
        {
            // Arrange
            var originalEntity = new Backend.Models.Match { Date = "1231.11.12" };

            // Act
            var action = new Action(() => Mlogic.Create(originalEntity));

            // Assert
            action.Should().NotThrow();
            mockMatches.Verify(m => m.Create(It.IsAny<Backend.Models.Match>()), Times.Once);

            var entities = Mlogic.ReadAll.ToList();
            entities.Should().NotBeNull();
            entities.Should().HaveCount(1);
            entities.First().Date.Should().Be("1231.11.12");

            var entity = Mlogic.ReadById(originalEntity.Id);
            entity.Should().NotBeNull();
            entity.Date.Should().Be("1231.11.12");

            originalEntity.Date = "1231.11.50";
            action = new Action(() => Mlogic.Update(originalEntity));
            action.Should().NotThrow();
            mockMatches.Verify(m => m.Update(It.IsAny<Backend.Models.Match>()), Times.Once);

            action = new Action(() => Mlogic.Delete(originalEntity.Id));
            action.Should().NotThrow();
            mockMatches.Verify(m => m.Delete(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void CRUD_User_Should_Work()
        {
            // Arrange
            var originalEntity = new User { UserName = "Test Entity", Email = "asd" };

            // Act
            var action = new Action(() => ULogic.Create(originalEntity));

            // Assert
            action.Should().NotThrow();
            mockUsers.Verify(m => m.Create(It.IsAny<User>()), Times.Once);

            var entities = ULogic.ReadAll.ToList();
            entities.Should().NotBeNull();
            entities.Should().HaveCount(1);
            entities.First().UserName.Should().Be("Test Entity");

            var entity = ULogic.ReadById(entities[0].Id);
            entity.Should().NotBeNull();
            entity.UserName.Should().Be("Test Entity");

            originalEntity.UserName = "Updated Entity";
            action = new Action(() => ULogic.Update(originalEntity));
            action.Should().NotThrow();
            mockUsers.Verify(m => m.Update(It.IsAny<User>()), Times.Once);

            action = new Action(() => ULogic.Delete(entities[0].Id));
            action.Should().NotThrow();
        }
    }
}
