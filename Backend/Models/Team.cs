using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public TournamentRegion Region { get; set; }
        public int Chemistry { get; set; }
        public int Rating { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public string UserId { get; set; }
        public int? TopId { get; set; }
        public int? JungleId { get; set; }
        public int? MidId { get; set; }
        public int? ADCId { get; set; }
        public int? SupportId { get; set; }

        [NotMapped]
        [JsonIgnore]
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Match> Matches { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Player> Players { get; set; }

        public Team()
        {
            Matches = new HashSet<Match>();
            Players = new HashSet<Player>();
        }

        public Team(string name, TournamentRegion region, int chemistry, int rating, int wins, int losses, User? user)
        {
            Name = name;
            Region = region;
            Chemistry = chemistry;
            Rating = rating;
            Wins = wins;
            Losses = losses;
            User = user;
            Matches = new HashSet<Match>();
            Players = new HashSet<Player>();
        }
    }
}