using System.Collections.Generic;

namespace TournamentReport.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Tournament> Tournaments { get; set; }
    }
}