using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TournamentReport.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public virtual ICollection<Round> Rounds { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Slug { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public User Owner { get; set; }
    }
}