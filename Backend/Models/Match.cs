using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Match
    {
        public Guid Id { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public string Date { get; set; }
        public string Tournament { get; set; }
        public int WinnerCredits { get; set; }
        public int LoserCredits { get; set; }
        public Team Winner { get; set; }
        public Team Loser { get; set; }
        public Match()
        {
        }
    }
}