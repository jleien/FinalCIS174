﻿using FinalCIS174.Models.UserLogin;
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
                new Class { ClassID = "fighter", Name = "Fighter" },
                new Class { ClassID = "wizard", Name = "Wizard" },
                new Class { ClassID = "paladin", Name = "Paladin" }
            );

            modelBuilder.Entity<Race>().HasData(
                new Race { RaceID = "human", Name = "Human" },
                new Race { RaceID = "elf", Name = "Elf" },
                new Race { RaceID = "orc", Name = "Orc" },
                new Race { RaceID = "dwarf", Name = "Dwarf" }
                );
            modelBuilder.Entity<Player>().HasData(
                new { PlayerID = "1", Name = "Finn Ahfsinguard", Level = 12, ClassID = "fighter", RaceID = "human" }
                );
        }
    }
}
