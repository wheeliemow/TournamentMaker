using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentReport.Models
{
    public class ReportScoreModel
    {
        public int Id { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
    }
}