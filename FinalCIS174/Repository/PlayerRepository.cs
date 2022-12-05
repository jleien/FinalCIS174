using FinalCIS174.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalCIS174.Repository
{
    public class PlayerRepository : IPlayerRepository<Player>
    {
        private DNDContext context;
        private DbSet<Player> dbset { get; set; }
        public PlayerRepository(DNDContext context)
        {
            this.context = context;
        }

        public virtual void AddPlayer(Player player) => dbset.Add(player);

        public virtual void DeletePlayer(Player player) => dbset.Remove(player);
        public virtual void EditPlayer(Player player) => dbset.Add(player);

        public List<Player> GetAllPlayers()
        {
            return context.Players.ToList();
        }

        public Player GetPlayerById(int id)
        {
            return context.Players.Find(id);
        }

        public virtual void Save() => context.SaveChanges();
    }
}
