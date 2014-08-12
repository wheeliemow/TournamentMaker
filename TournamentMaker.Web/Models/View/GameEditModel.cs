using System;

namespace TournamentReport.Models
{
    public class GameEditModel
    {
        public int Id { get; set; }
        public int? HomeTeamId { get; set; }
        public int? AwayTeamId { get; set; }
        public DateTime? GameTime { get; set; }
    }
}