using System.Data.Entity.Migrations;
using System.Web.DynamicData;
using DynamicData.EFCodeFirstProvider;
using DynamicDataEFCodeFirst;
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
            RegisterDynamicData();
        }

        private static void RunDatabaseMigrations()
        {
            var dbMigrator = new DbMigrator(new MigrationsConfiguration());
            dbMigrator.Update();
        }

        private static void RegisterDynamicData()
        {
            Registration.DefaultModel.RegisterContext(
                new EFCodeFirstDataModelProvider(() => new TournamentContext()),
                new ContextConfiguration {ScaffoldAllTables = true});
        }
    }
}