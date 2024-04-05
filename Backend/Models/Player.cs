using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Rating { get; set; }
        public RoleEnum Role { get; set; }
        public PlayerRegion Region { get {
                switch (Nationality)
                { 
                    default:
                        break;
                }
                return PlayerRegion.AMERICAS;
            } set { } }
        public string Language { get; set; }
        public string Nationality { get; set; }
        public int YearsAsPro { get; set; }
        [ForeignKey("Team")]
        public int TeamId { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Team Team { get; set; }
        public Player()
        {
        }

        public Player(string name, string image, int rating, RoleEnum role, string language, string nationality, int age, int teamId)
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
    }
}
