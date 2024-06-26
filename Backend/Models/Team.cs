using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class Team : IModelBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public RegionEnum Region { get; set; }
        public int Chemistry { get { return CalculateChemistry(); } set { } }
        public int Rating { get { return CalculateRating(); } set { } }
        public int Wins { get { return Matches.Count(x => x.WinnerId == Id); } set { } }
        public int Losses { get { return Matches.Count(x => x.LoserId == Id); } set { } }
        public string? UserId { get; set; }
        public int? TopId { get; set; }
        public int? JungleId { get; set; }
        public int? MidId { get; set; }
        public int? ADCId { get; set; }
        public int? SupportId { get; set; }
        public bool HasActiveMatch { get { return CheckActiveMatch(); } set { } }

        [NotMapped]
        [JsonIgnore]
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

        public Team(string name, RegionEnum region, int rating, int wins, int losses, User? user)
        {
            Name = name;
            Region = region;
            Rating = rating;
            Wins = wins;
            Losses = losses;
            User = user;
            Matches = new HashSet<Match>();
            Players = new HashSet<Player>();
        }

        private int CalculateChemistry()
        {
            if (Players is not null && Players.Count == 5)
            {
                int chemistry = 0;
                int distinctLangs = 5 - Players.Select(p => p.Language).Distinct().Count();
                int distinctNationality = 5 - Players.Select(p => p.Nationality).Distinct().Count();
                chemistry += distinctLangs;
                chemistry += distinctNationality;
                CheckRole(ref chemistry);

                return chemistry * 100 / 15;
            }
            return 0;
        }

        private void CheckRole(ref int chemistry)
        {
            if (Players is not null && Players.Count() is not 0)
            {
                foreach (Player player in Players)
                {
                    switch (player.Role)
                    {
                        case RoleEnum.Top:
                            if (player.Id == TopId)
                            {
                                chemistry += 1;
                            }
                            break;
                        case RoleEnum.Jungle:
                            if (player.Id == JungleId)
                            {
                                chemistry += 1;
                            }
                            break;
                        case RoleEnum.Mid:
                            if (player.Id == TopId)
                            {
                                chemistry += 1;
                            }
                            break;
                        case RoleEnum.ADC:
                            if (player.Id == ADCId)
                            {
                                chemistry += 1;
                            }
                            break;
                        case RoleEnum.Support:
                            if (player.Id == SupportId)
                            {
                                chemistry += 1;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private int CalculateRating()
        {
            Rating = 0;
            if (Players is not null && Players.Count() != 0)
            {
                foreach (Player player in Players)
                {
                    Rating += player.Rating;
                }
                Rating += Chemistry;
                return Rating * 100 / 600;
            }
            return 0;
        }

        private bool CheckActiveMatch()
        {
            if (Matches is not null && Matches.Count() is not 0)
            {
                return Matches.FirstOrDefault(x => x.IsDone == false) == null ? false : true;
            }
            return false;
        }
    }
}