using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentReport.Models
{
    public class Field
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}