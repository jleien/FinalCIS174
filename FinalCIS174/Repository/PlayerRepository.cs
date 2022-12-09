using FinalCIS174.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalCIS174.Repository
{
    public class PlayerRepository : IPlayerRepository<Player>
    {
        private DNDContext context;
        public PlayerRepository(DNDContext context)
        {
            this.context = context;
        }

        public void AddPlayer(Player player) => context.Add(player);

        public void DeletePlayer(Player player) => context.Remove(player);
        public void EditPlayer(Player player)
        {
            context.Remove(GetPlayerById(player.PlayerID));
            context.Add(player);
        }

        public List<Player> GetAllPlayers()
        {
            return context.Players.ToList();
        }

        public Player GetPlayerById(string id)
        {
            return context.Players.Find(id);
        }

        public void Save() => context.SaveChanges();
        public int Count()
        {
            return context.Players.Count();
        }
    }
}
