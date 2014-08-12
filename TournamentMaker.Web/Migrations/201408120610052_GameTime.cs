namespace TournamentReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GameTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "GameTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "GameTime");
        }
    }
}
