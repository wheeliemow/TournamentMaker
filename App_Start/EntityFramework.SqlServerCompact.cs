using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TournamentReport.Models;

[assembly: WebActivator.PreApplicationStartMethod(typeof(TournamentReport.App_Start.EntityFramework_SqlServerCompact), "Start")]

namespace TournamentReport.App_Start {
    public static class EntityFramework_SqlServerCompact {
        public static void Start() {
            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
            Database.SetInitializer(new MyDropCreateDatabaseIfModelChanges());
        }
    }

    public class MyDropCreateDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<TournamentContext> {
        protected override void Seed(TournamentContext context) {
            var tournament = new Tournament { Name = "Timbers Corporate Cup" };
            context.Tournaments.Add(tournament);
            context.SaveChanges();

            var nike = new Team { Name = "Nike", Group = "Group 1" };
            var microsoft = new Team { Name = "Microsoft", Group = "Group 1" };
            var cmd = new Team { Name = "CMD", Group = "Group 1" };
            var ups = new Team { Name = "UPS", Group = "Group 2" };
            var adidas = new Team { Name = "Adidas", Group = "Group 2" };
            var wiedenKenedy = new Team { Name = "Wieden + Kennedy", Group = "Group 2" };
            context.Teams.Add(nike);
            context.Teams.Add(microsoft);
            context.Teams.Add(cmd);
            context.Teams.Add(ups);
            context.Teams.Add(adidas);
            context.Teams.Add(wiedenKenedy);
            context.SaveChanges();

            var roundOne = new Round { Name = "Mens - Round One (PCC Rock Creek)", Tournament = tournament };
            var roundTwo = new Round { Name = "Mens - Round Two (PCC Rock Creek)", Tournament = tournament };
            var roundThree = new Round { Name = "Mens - Round Three (PCC Rock Creek)", Tournament = tournament };

            context.Rounds.Add(roundOne);
            context.Rounds.Add(roundTwo);
            context.Rounds.Add(roundThree);
            context.SaveChanges();

            context.Games.Add(new Game { Round = roundOne, Teams = new List<Team> { nike, cmd } });
            context.Games.Add(new Game { Round = roundOne, Teams = new List<Team> { microsoft, adidas } });
            context.Games.Add(new Game { Round = roundOne, Teams = new List<Team> { ups, wiedenKenedy } });
            context.Games.Add(new Game { Round = roundTwo, Teams = new List<Team> { nike, ups } });
            context.Games.Add(new Game { Round = roundTwo, Teams = new List<Team> { adidas, wiedenKenedy } });
            context.Games.Add(new Game { Round = roundTwo, Teams = new List<Team> { microsoft, cmd } });
            context.Games.Add(new Game { Round = roundThree, Teams = new List<Team> { adidas, ups } });
            context.Games.Add(new Game { Round = roundThree, Teams = new List<Team> { cmd, wiedenKenedy } });
            context.Games.Add(new Game { Round = roundThree, Teams = new List<Team> { nike, microsoft } });
            context.SaveChanges();
        }
    }
}
