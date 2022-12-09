using FinalCIS174.Models;
using FinalCIS174.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Numerics;

namespace FinalCIS174Tests
{
    public class PlayerTests
    {
        //Players used for testing
        Player testPlayer = new Player { PlayerID = "1", Name = "TestPlayer", Level = 1, ClassID = "fighter", RaceID = "human", CreatorOfCharacter = "DIO" };
        Player testPlayer2 = new Player { PlayerID = "2", Name = "TestPlayer2", Level = 2, ClassID = "paladin", RaceID = "orc", CreatorOfCharacter = "DIO" };
        Player testPlayer3 = new Player { PlayerID = "3", Name = "TestPlayer3", Level = 3, ClassID = "standuser", RaceID = "elf", CreatorOfCharacter = "DIO" };

        DbContextOptions<DNDContext> inmemory;
        public PlayerTests()
        {
            inmemory = new DbContextOptionsBuilder<DNDContext>()
                .UseInMemoryDatabase("Filename=test.db")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
        }
        [Fact]
        public void AddPlayer_Test()
        {
            //Test to add player to database
            DNDContext context = new DNDContext(inmemory);
            var repo = new PlayerRepository(context);
            repo.AddPlayer(testPlayer);
            repo.AddPlayer(testPlayer2);
            repo.Save();
            var actual = repo.Count();
            var expected = 2;
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void EditPlayer_Test()
        {
            //Test to edit player in database
            DNDContext context = new DNDContext(inmemory);
            var repo = new PlayerRepository(context);
            testPlayer.Name = "Test Name Change 1";
            repo.EditPlayer(testPlayer);
            repo.Save();
            var actual = repo.GetPlayerById("1").Name;
            var expected = "Test Name Change 1";
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void DeletePlayer_Test()
        {
            //Test to delete player from database
            DNDContext context = new DNDContext(inmemory);
            var repo = new PlayerRepository(context);
            repo.AddPlayer(testPlayer3);
            repo.Save();
            repo.DeletePlayer(testPlayer3);
            repo.Save();
            var actual = repo.GetPlayerById("3");
            
            Assert.Equal(null, actual);
        }
    }
}