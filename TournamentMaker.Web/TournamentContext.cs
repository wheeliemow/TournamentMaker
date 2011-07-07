using System.Data.Entity;
using TournamentReport.Models;

namespace TournamentReport {
    public class TournamentContext : DbContext {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<TournamentReport.TournamentContext>());

        public DbSet<Team> Teams { get; set; }

        public DbSet<Round> Rounds { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Tournament> Tournaments { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
