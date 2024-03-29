using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Credits { get; set; }
        [ForeignKey("Team")]
        public int TeamId { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Team Team { get; set; }
        public User()
        {
        }
    }
}
