using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class User : IdentityUser, IModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ContentType { get; set; }
        public byte[]? Data { get; set; }
        public int Credits { get; set; }
        public int TeamId { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Team Team { get; set; }

        public User()
        {
            Credits = 0;
            Team = new Team();
        }

        public User(string userName) : base(userName)
        {
            Credits = 0;
            Team = new Team();
        }
    }
}
