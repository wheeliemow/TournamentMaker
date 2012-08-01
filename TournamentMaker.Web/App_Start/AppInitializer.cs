using System.Data.Entity.Migrations;
using TournamentReport.App_Start;
using TournamentReport.Migrations;

[assembly: WebActivator.PostApplicationStartMethod(typeof (AppInitializer), "PostStart")]

namespace TournamentReport.App_Start
{
    public class AppInitializer
    {
        public static void PostStart()
        {
            RunDatabaseMigrations();
        }

        private static void RunDatabaseMigrations()
        {
            var dbMigrator = new DbMigrator(new MigrationsConfiguration());
            dbMigrator.Update();
        }
    }
}