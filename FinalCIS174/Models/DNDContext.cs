using FinalCIS174.Models.UserLogin;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace FinalCIS174.Models
{
    public class DNDContext : IdentityDbContext<User>
    {
        public DNDContext(DbContextOptions<DNDContext> options) : base(options) { }

        public DbSet<Player>? Players { get; set; }
        public DbSet<Class>? Classes { get; set; }
        public DbSet<Race>? Races { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Class>().HasData(
                new Class { ClassID = "barbarian", Name = "Barbarian" },
                new Class { ClassID = "bard", Name = "Bard" },
                new Class { ClassID = "cleric", Name = "Cleric" },
                new Class { ClassID = "druid", Name = "Druid" },
                new Class { ClassID = "fighter", Name = "Fighter" },
                new Class { ClassID = "gunzerker", Name = "Gunzerker" },
                new Class { ClassID = "monk", Name = "Monk" },
                new Class { ClassID = "standuser", Name = "Stand-User" },
                new Class { ClassID = "wizard", Name = "Wizard" },
                new Class { ClassID = "operator", Name = "Operator" },
                new Class { ClassID = "paladin", Name = "Paladin" }
                //test comment

            );

            modelBuilder.Entity<Race>().HasData(
                new Race { RaceID = "human", Name = "Human" },
                new Race { RaceID = "aasimar", Name = "Aasimar" },
                new Race { RaceID = "celestial", Name = "Celestial" },
                new Race { RaceID = "terra", Name = "Terra" },
                new Race { RaceID = "tabaxi", Name = "Tabaxi" },
                new Race { RaceID = "elf", Name = "Elf" },
                new Race { RaceID = "tiefling", Name = "Tiefling" },
                new Race { RaceID = "orc", Name = "Orc" },
                new Race { RaceID = "plasmoid", Name = "Plasmoid" },
                new Race { RaceID = "dwarf", Name = "Dwarf" },
                //test
                new Race { RaceID = "gnome", Name = "Gnome" },
                new Race { RaceID = "half-elf", Name = "Half-Elf" },
                new Race { RaceID = "half-orc", Name = "Half-Orc" },
                new Race { RaceID = "dragonborn", Name = "Dragonborn" }

                );
            //sample character
            modelBuilder.Entity<Player>().HasData(
                new { PlayerID = "1", Name = "Finn Ahfsinguard", Level = 12, ClassID = "fighter", RaceID = "human", CreatorOfCharacter="DIO" }
                );
        }
    }
}
