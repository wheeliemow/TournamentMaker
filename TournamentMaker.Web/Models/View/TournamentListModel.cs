using System.Collections.Generic;
using System.Linq;

namespace TournamentReport.Models
{
    public class TournamentListModel
    {
        public TournamentListModel(IEnumerable<Tournament> tournaments)
        {
            var sortedTournaments = tournaments.OrderByDescending(t => t.GetYear()).ThenBy(t => t.Id);
            var maxYear = sortedTournaments.Max(t => t.GetYear());
            RecentTournaments = sortedTournaments.Where(t => t.GetYear() == maxYear).ToList();
            ArchivedTournaments = sortedTournaments.Where(t => t.GetYear() < maxYear).ToList();
        }

        public ICollection<Tournament> RecentTournaments { get; private set; }

        public ICollection<Tournament> ArchivedTournaments { get; private set; }
    }
}