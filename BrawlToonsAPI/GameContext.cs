﻿using BrawlToonsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BrawlToonsAPI
{
    public class GameContext : DbContext
    {
        public DbSet<Player> players { get; set; }

        public DbSet<Matches> matches { get; set; }

        public DbSet<Characters> characters { get; set; }

        public DbSet<PlayerCharacter> player_character { get; set; }

        public GameContext(DbContextOptions<GameContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasKey(p => p.player_id);
            modelBuilder.Entity<Matches>()
                .HasKey(m => m.match_id);
            modelBuilder.Entity<Characters>()
                .HasKey(c=>c.character_id);
            modelBuilder.Entity<PlayerCharacter>()
                .HasKey(pc => new { pc.player_id, pc.character_id });
        }
    }
}
