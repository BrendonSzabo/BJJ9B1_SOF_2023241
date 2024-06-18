using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class Match : IModelBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public string Date { get; set; }
        public RegionEnum Region { get; set; }
        public int WinnerCredits { get; set; }
        public int LoserCredits { get; set; }
        public int? WinnerId { get; set; }
        public int? LoserId { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public Match()
        {
            Team1Score = 0;
            Team2Score = 0;
            Date = "";
            WinnerCredits = 0;
            LoserCredits = 0;
            Teams = new HashSet<Team>();
        }
    }
}