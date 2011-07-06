using System.Collections.Generic;

namespace TournamentReport.Models {
    public class StandingsViewModel {
        public IEnumerable<Team> Teams { get; set; }
        public IEnumerable<Game> Games { get; set; }
    }
}