
using System.Collections.Generic;
namespace TournamentReport.Models {
    public class Tournament {
        public int Id { get; set; }
        public virtual ICollection<Round> Rounds { get; set; }
        public string Name { get; set; }
    }
}