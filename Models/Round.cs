using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TournamentReport.Models {
    [DisplayColumn("Name")]
    public class Round {
        public int Id { get; set; }
        public string Name { get; set; }
        public Tournament Tournament { get; set; }
        public int TournamentId { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}