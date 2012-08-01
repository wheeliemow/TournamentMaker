using System.Data.Entity;
using TournamentReport.Models;

namespace TournamentReport
{
    public class TournamentContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }

        public DbSet<Round> Rounds { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Tournament> Tournaments { get; set; }

        public DbSet<User> Users { get; set; }
    }
}