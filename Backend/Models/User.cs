using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class User : IdentityUser, IModelBase
    {
        public string? ContentType { get; set; }
        /// <summary>
        /// The profile image of the user.
        /// </summary>
        public byte[]? Data { get; set; }
        public int Credits { get { return CalculateCredits(); } set { } }
        public int Rating { get { return CalculateRating(); } set { } }
        public int TeamId { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Team Team { get; set; }

        public User()
        {
            Team = new Team();
        }

        private int CalculateRating()
        {
            if (Team is not null && Team.Matches is not null && Team.Matches.Count() is not 0)
            {
                int totalMatches = Team.Matches.Where(x => x.WinnerId == Team.Id).Count() + Team.Matches.Where(x => x.LoserId == Team.Id).Count();
                int wins = Team.Matches.Where(x => x.WinnerId == Team.Id).Count();
                return (int)((double)wins / totalMatches * 100);
            }
            return 0;
        }

        private int CalculateCredits()
        {
            int credits = 0;
            if (Team is not null && Team.Matches is not null && Team.Matches.Count() is not 0)
            {
                foreach (var match in Team.Matches)
                {
                    if (match.WinnerId == Team.Id)
                    {
                        credits += match.WinnerCredits;
                    }
                    else
                    {
                        credits += match.LoserCredits;
                    }
                }
                if (Team.Players is not null && Team.Players.Count() != 0)
                {
                    foreach (var player in Team.Players)
                    {
                        credits -= player.Price;
                    }
                }
            }
            return credits;
        }
    }
}
