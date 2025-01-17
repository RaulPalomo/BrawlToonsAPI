using BrawlToonsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BrawlToonsAPI
{
    public class GameContext : DbContext
    {
        public DbSet<Player> players { get; set; }

        public DbSet<Matches> matches { get; set; }

        public GameContext(DbContextOptions<GameContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasKey(p => p.player_id);
            modelBuilder.Entity<Matches>()
                .HasKey(m => m.match_id);
        }
    }
}
