using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentReport.Models {
    public class GameResult {
        public GameResult(int thisTeamScore, int otherTeamScore) {
            ThisTeamScore = thisTeamScore;
            OtherTeamScore = otherTeamScore;
        }
        public int ThisTeamScore { get; private set; }
        public int OtherTeamScore { get; private set; }
    }
}