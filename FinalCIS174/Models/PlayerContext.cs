using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace FinalCIS174.Models
{
    public class PlayerContext :DbContext
    {
        public PlayerContext(DbContextOptions<DNDContext> options) : base(options) { }

        public DbSet<Player>? Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().HasData(
                new {PlayerID = "1", Name = "Finn Ahfsinguard", Level = 12, Class = "Fighter", Race = "Variant Human"} 
                );
        }
          
    }
}
