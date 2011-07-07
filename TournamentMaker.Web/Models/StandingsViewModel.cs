using System;
using System.Collections.Generic;
using System.Linq;

namespace TournamentReport.Models {
    public class StandingsViewModel {
        public string Title { get; set; }
        public IEnumerable<Team> Teams { get; set; }
        public IEnumerable<Round> Rounds { get; set; }

        public IEnumerable<IGrouping<string, Team>> Groups {
            get {
                if (Teams.Any(t => !String.IsNullOrEmpty(t.Group))) {
                    return Teams.GroupBy(t => t.Group);
                }
                return Enumerable.Empty<IGrouping<string, Team>>();
            }
        }
    }
}