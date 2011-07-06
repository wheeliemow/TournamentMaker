using System.Collections.Generic;
using System.Linq;
namespace TournamentReport.Models {
    public class Team {
        private bool _standingsCalculated = false;
        public int Id { get; set; }
        public virtual ICollection<Game> Games { get; set; }
        public string Name { get; set; }
        public int Wins { get; private set; }
        public int Losses { get; private set; }
        public int Ties { get; private set; }
        public string Group { get; set; }
        public int GamesPlayed {
            get {
                return Games.Count(g => g.HomeTeamScore != null && g.InGame(this));
            }
        }
        public int Points {
            get {
                CalculateWinsLosses();
                return Wins * 3 + Ties;
            }
        }

        private void CalculateWinsLosses() {
            if (!_standingsCalculated) {
                Wins = 0;
                Losses = 0;
                Ties = 0;
                foreach (var game in Games) {
                    // This is home team
                    if (game.HomeTeam.Id == this.Id) {
                        if (game.HomeTeamScore > game.AwayTeamScore) {
                            Wins++;
                        }
                        else if (game.HomeTeamScore < game.AwayTeamScore) {
                            Losses++;
                        }
                        else if (game.HomeTeamScore != null) {
                            Ties++;
                        }
                    }
                    else if (game.AwayTeam.Id == this.Id) {
                        if (game.HomeTeamScore < game.AwayTeamScore) {
                            Wins++;
                        }
                        else if (game.HomeTeamScore > game.AwayTeamScore) {
                            Losses++;
                        }
                        else if (game.HomeTeamScore != null) {
                            Ties++;
                        }
                    }
                }
            }
            _standingsCalculated = true;
        }
    }
}