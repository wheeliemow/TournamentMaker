using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TournamentReport.Models;
using WebMatrix.WebData;

[assembly: WebActivator.PreApplicationStartMethod(typeof(TournamentReport.App_Start.EntityFramework_SqlServerCompact), "Start")]

namespace TournamentReport.App_Start {
    public static class EntityFramework_SqlServerCompact {
        internal const string DefaultUserName = "Chad";

        public static void Start() {
            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
            var initializer = new MyDropCreateDatabaseIfModelChanges();

            if (!Database.Exists("TournamentReport.TournamentContext")) {
                using (var context = new TournamentContext()) {
                    var defaultUser = new User { Name = DefaultUserName };
                    context.Users.Add(defaultUser);
                    context.SaveChanges();

                    initializer.SeedData(context);
                }
            }

            Database.SetInitializer(initializer);
        }
    }

    public class MyDropCreateDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<TournamentContext> {
        public void SeedData(TournamentContext context) {
            Seed(context);
        }
        
        protected override void Seed(TournamentContext context) {
            var owner = context.Users.FirstOrDefault(u => u.Name == EntityFramework_SqlServerCompact.DefaultUserName);

            SetupPortlandsCupMensTournament(context, owner);
            SetupPortlandsCupCoedTournament(context, owner);
        }

        private static void SetupPortlandsCupMensTournament(TournamentContext context, User owner) {
            var mensTournament = new Tournament { Name = "2011 Timbers Corporate Cup - Mens", Slug = "2011-timbers-corp-cup-mens", Owner = owner};
            context.Tournaments.Add(mensTournament);
            context.SaveChanges();

            var nike = new Team { Name = "Nike", Group = "Group 1", Tournament = mensTournament };
            var microsoft = new Team { Name = "Microsoft", Group = "Group 1", Tournament = mensTournament };
            var cmd = new Team { Name = "CMD", Group = "Group 1", Tournament = mensTournament };
            var ups = new Team { Name = "UPS", Group = "Group 2", Tournament = mensTournament };
            var adidas = new Team { Name = "Adidas", Group = "Group 2", Tournament = mensTournament };
            var wiedenKenedy = new Team { Name = "Wieden + Kennedy", Group = "Group 2", Tournament = mensTournament };
            context.Teams.Add(nike);
            context.Teams.Add(microsoft);
            context.Teams.Add(cmd);
            context.Teams.Add(ups);
            context.Teams.Add(adidas);
            context.Teams.Add(wiedenKenedy);
            context.SaveChanges();

            var roundOne = new Round { Name = "Mens - Round One (PCC Rock Creek)", Tournament = mensTournament };
            var roundTwo = new Round { Name = "Mens - Round Two (PCC Rock Creek)", Tournament = mensTournament };
            var roundThree = new Round { Name = "Mens - Round Three (PCC Rock Creek)", Tournament = mensTournament };

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

        private static void SetupPortlandsCupCoedTournament(TournamentContext context, User owner) {
            var coedTournament = new Tournament { Name = "2011 Timbers Corporate Cup - Coed", Slug = "2011-timbers-corp-cup-coed", Owner = owner };
            context.Tournaments.Add(coedTournament);
            context.SaveChanges();

            var adidas = new Team { Name = "Adidas", Tournament = coedTournament };
            var microsoft = new Team { Name = "Microsoft", Tournament = coedTournament };
            var nike = new Team { Name = "Nike", Tournament = coedTournament };
            var tursis = new Team { Name = "Tursi's Soccer Supply", Tournament = coedTournament };

            context.Teams.Add(adidas);
            context.Teams.Add(microsoft);
            context.Teams.Add(nike);
            context.Teams.Add(tursis);
            context.SaveChanges();

            var roundOne = new Round { Name = "Mens - Round One (PCC Rock Creek)", Tournament = coedTournament };
            var roundTwo = new Round { Name = "Mens - Round Two (PCC Rock Creek)", Tournament = coedTournament };
            var roundThree = new Round { Name = "Mens - Round Three (PCC Rock Creek)", Tournament = coedTournament };
            var thirdPlaceRound = new Round { Name = "Third Place Finals", Tournament = coedTournament };
            var championshipRound = new Round { Name = "Championship", Tournament = coedTournament };

            context.Rounds.Add(roundOne);
            context.Rounds.Add(roundTwo);
            context.Rounds.Add(roundThree);
            context.Rounds.Add(thirdPlaceRound);
            context.Rounds.Add(championshipRound);
            context.SaveChanges();

            context.Games.Add(new Game { Round = roundOne, Teams = new List<Team> { adidas, nike} });
            context.Games.Add(new Game { Round = roundOne, Teams = new List<Team> { microsoft, tursis} });
            context.Games.Add(new Game { Round = roundTwo, Teams = new List<Team> { nike, tursis} });
            context.Games.Add(new Game { Round = roundTwo, Teams = new List<Team> { microsoft, adidas} });
            context.Games.Add(new Game { Round = roundThree, Teams = new List<Team> { nike, microsoft} });
            context.Games.Add(new Game { Round = roundThree, Teams = new List<Team> { tursis, adidas} });
            context.Games.Add(new Game { Round = thirdPlaceRound });
            context.Games.Add(new Game { Round = championshipRound });
            context.SaveChanges();
        }
    }
}
