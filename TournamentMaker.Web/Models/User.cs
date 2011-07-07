using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentReport.Models {
    public class User {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Tournament> Tournaments { get; set; }
    }
}