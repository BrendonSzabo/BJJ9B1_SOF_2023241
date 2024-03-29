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
        public int Rating { get; set; }
        public RoleEnum Role { get; set; }
        public string Language { get; set; }
        public string Nationality { get; set; }
        public int Experience { get; set; }
        public int Age { get; set; }
        [ForeignKey("Team")]
        public int TeamId { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Team Team { get; set; }
        public Player()
        {
        }
    }
}
