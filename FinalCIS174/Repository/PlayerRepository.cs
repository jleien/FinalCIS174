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

        public virtual void AddPlayer(Player player) => context.Add(player);

        public virtual void DeletePlayer(Player player) => context.Remove(player);
        public virtual void EditPlayer(Player player) => context.Add(player);

        public List<Player> GetAllPlayers()
        {
            return context.Players.ToList();
        }

        public Player GetPlayerById(int id)
        {
            return context.Players.Find(id);
        }

        public virtual void Save() => context.SaveChanges();

        public int Count()
        {
            return context.Players.Count();
        }
    }
}
