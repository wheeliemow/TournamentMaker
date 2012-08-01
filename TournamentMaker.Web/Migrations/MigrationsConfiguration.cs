using System.Data.Entity.Migrations;
using System.Linq;

namespace TournamentReport.Migrations
{
    internal sealed class MigrationsConfiguration : DbMigrationsConfiguration<TournamentContext>
    {
        public MigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TournamentContext context)
        {
            var games = from game in context.Games
                        where game.AwayTeamId == null || game.HomeTeamId == null
                        select game;
            foreach (var game in games)
            {
                if (game.Teams != null && game.Teams.Count() == 2)
                {
                    game.HomeTeamId = game.Teams.First().Id;
                    game.AwayTeamId = game.Teams.Last().Id;
                }
            }

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}