using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public Role Role { get; set; }
        public string[] Language { get; set; }
        public string Nationality { get; set; }
        public int Experience { get; set; }
        public int Age { get; set; }
        public Guid TeamId { get; set; }
        public Team Team { get; set; }
        public Player()
        {
        }
    }
}
