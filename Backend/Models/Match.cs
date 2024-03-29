using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class Match
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public string Date { get; set; }
        public string Tournament { get; set; }
        public int WinnerCredits { get; set; }
        public int LoserCredits { get; set; }
        public int WinnerId { get; set; }
        public int LoserId { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Team> Teams { get; set; }

        public Match()
        {
            Teams = new HashSet<Team>();
        }
    }
}