using System.Data.Entity.Migrations;

namespace TournamentReport.Migrations
{
    public partial class HomeAwayTeamId : DbMigration
    {
        public override void Up()
        {
            AddColumn("Games", "HomeTeamId", c => c.Int());
            AddColumn("Games", "AwayTeamId", c => c.Int());
        }

        public override void Down()
        {
            DropColumn("Games", "AwayTeamId");
            DropColumn("Games", "HomeTeamId");
        }
    }
}