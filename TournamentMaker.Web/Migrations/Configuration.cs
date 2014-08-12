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
            //  This method will be called after migrating to the latest version.
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

            var gamesWithoutFields = from game in context.Games
                where game.FieldId == null
                select game;
            var defaultField = (from field in context.Fields
                                where field.Name == "TBD"
                select field).FirstOrDefault();
            if (defaultField != null)
            {
                foreach (var game in gamesWithoutFields)
                {
                    game.FieldId = defaultField.Id;
                }
            }
            context.SaveChanges();
        }
    }
}
