using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class Player : IModelBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Rating { get; set; }
        public int Price { get { return CalculatePrice(); } set { } }
        public RoleEnum Role { get; set; }
        public RegionEnum Region { get { return Team == null ? RegionEnum.NotInATeam : Team.Region; } set { } }
        public string Language { get; set; }
        public NationalityEnum Nationality { get; set; }
        public int YearsAsPro { get; set; }
        [ForeignKey("Team")]
        public int? TeamId { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Team? Team { get; set; }
        public Player()
        {
        }

        public Player(string name, string image, int rating, RoleEnum role, string language, NationalityEnum nationality, int age, int teamId)
        {
            Name = name;
            Image = image;
            Rating = rating;
            Role = role;
            Language = language;
            Nationality = nationality;
            YearsAsPro = age;
            TeamId = teamId;
        }

        private int CalculatePrice()
        {
            int basePrice = 1000;

            basePrice += Rating * 500;
            basePrice += YearsAsPro * 200;

            switch (Region)
            {
                case RegionEnum.LCS:
                    basePrice += 1000;
                    break;
                case RegionEnum.LEC:
                    basePrice += 1000;
                    break;
                case RegionEnum.LCK:
                    basePrice += 2000;
                    break;
                case RegionEnum.LPL:
                    basePrice += 2000;
                    break;
                case RegionEnum.PCS:
                    basePrice += 900;
                    break;
                case RegionEnum.LCO:
                    basePrice += 500;
                    break;
                case RegionEnum.CBLOL:
                    basePrice += 500;
                    break;
                case RegionEnum.LJL:
                    basePrice += 500;
                    break;
                case RegionEnum.LLA:
                    basePrice += 500;
                    break;
                case RegionEnum.VCS:
                    basePrice += 900;
                    break;
                default:
                    basePrice += 0;
                    break;
            }

            if (basePrice < 0)
            {
                basePrice = 0;
            }

            return basePrice;
        }

    }
}
