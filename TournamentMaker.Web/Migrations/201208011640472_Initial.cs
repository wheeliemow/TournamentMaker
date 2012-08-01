using System.Data.Entity.Migrations;

namespace TournamentReport.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Teams",
                c => new
                     {
                         Id = c.Int(nullable: false, identity: true),
                         Group = c.String(maxLength: 4000),
                         Name = c.String(maxLength: 4000),
                         Wins = c.Int(nullable: false),
                         Losses = c.Int(nullable: false),
                         Ties = c.Int(nullable: false),
                         GoalsScored = c.Int(nullable: false),
                         GoalsAgainst = c.Int(nullable: false),
                         Tournament_Id = c.Int(),
                     })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Tournaments", t => t.Tournament_Id)
                .Index(t => t.Tournament_Id);

            CreateTable(
                "Games",
                c => new
                     {
                         Id = c.Int(nullable: false, identity: true),
                         HomeTeamScore = c.Int(),
                         AwayTeamScore = c.Int(),
                         RoundId = c.Int(nullable: false),
                     })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Rounds", t => t.RoundId, cascadeDelete: true)
                .Index(t => t.RoundId);

            CreateTable(
                "Rounds",
                c => new
                     {
                         Id = c.Int(nullable: false, identity: true),
                         Name = c.String(maxLength: 4000),
                         TournamentId = c.Int(nullable: false),
                     })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Tournaments", t => t.TournamentId, cascadeDelete: true)
                .Index(t => t.TournamentId);

            CreateTable(
                "Tournaments",
                c => new
                     {
                         Id = c.Int(nullable: false, identity: true),
                         Name = c.String(nullable: false, maxLength: 4000),
                         Slug = c.String(nullable: false, maxLength: 4000),
                         Description = c.String(maxLength: 4000),
                         Owner_Id = c.Int(),
                     })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Users", t => t.Owner_Id)
                .Index(t => t.Owner_Id);

            CreateTable(
                "Users",
                c => new
                     {
                         Id = c.Int(nullable: false, identity: true),
                         Name = c.String(maxLength: 4000),
                     })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "GameTeams",
                c => new
                     {
                         Game_Id = c.Int(nullable: false),
                         Team_Id = c.Int(nullable: false),
                     })
                .PrimaryKey(t => new {t.Game_Id, t.Team_Id})
                .ForeignKey("Games", t => t.Game_Id, cascadeDelete: true)
                .ForeignKey("Teams", t => t.Team_Id, cascadeDelete: true)
                .Index(t => t.Game_Id)
                .Index(t => t.Team_Id);
        }

        public override void Down()
        {
            DropIndex("GameTeams", new[] {"Team_Id"});
            DropIndex("GameTeams", new[] {"Game_Id"});
            DropIndex("Tournaments", new[] {"Owner_Id"});
            DropIndex("Rounds", new[] {"TournamentId"});
            DropIndex("Games", new[] {"RoundId"});
            DropIndex("Teams", new[] {"Tournament_Id"});
            DropForeignKey("GameTeams", "Team_Id", "Teams");
            DropForeignKey("GameTeams", "Game_Id", "Games");
            DropForeignKey("Tournaments", "Owner_Id", "Users");
            DropForeignKey("Rounds", "TournamentId", "Tournaments");
            DropForeignKey("Games", "RoundId", "Rounds");
            DropForeignKey("Teams", "Tournament_Id", "Tournaments");
            DropTable("GameTeams");
            DropTable("Users");
            DropTable("Tournaments");
            DropTable("Rounds");
            DropTable("Games");
            DropTable("Teams");
        }
    }
}