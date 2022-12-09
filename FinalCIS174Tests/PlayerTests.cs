using FinalCIS174.Models;
using FinalCIS174.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Numerics;

namespace FinalCIS174Tests
{
    public class PlayerTests
    {
        Player testPlayer = new Player { PlayerID = "1", Name = "TestPlayer", Level = 1, ClassID = "fighter", RaceID = "human", CreatorOfCharacter = "DIO" };
        Player testPlayer2 = new Player { PlayerID = "2", Name = "TestPlayer2", Level = 2, ClassID = "paladin", RaceID = "orc", CreatorOfCharacter = "DIO" };

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
            Assert.Equal(2, repo.Count());
        }
    }
}