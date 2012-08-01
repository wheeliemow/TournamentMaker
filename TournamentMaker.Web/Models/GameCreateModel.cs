using System.ComponentModel.DataAnnotations;

namespace TournamentReport.Models
{
    public class GameCreateModel
    {
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        [Required]
        public int? RoundId { get; set; }
    }
}