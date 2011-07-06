using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TournamentReport.Models {
    public class Game {
        public int Id { get; set; }
        public ICollection<Team> Teams { get; set; }

        public Team HomeTeam {
            get {
                if (Teams == null) {
                    return null;
                }
                return Teams.FirstOrDefault();
            }
        }

        public Team AwayTeam {
            get {
                if (Teams == null) {
                    return null;
                }
                return Teams.LastOrDefault();
            }
        }

        [Display(Name = "Home Team Score")]
        public int? HomeTeamScore { get; set; }
        [Display(Name = "Away Team Score")]
        public int? AwayTeamScore { get; set; }
        public Round Round { get; set; }
        public int RoundId { get; set; }

        public bool InGame(Team team) {
            return team.Id == HomeTeam.Id || team.Id == AwayTeam.Id;
        }
    }
}