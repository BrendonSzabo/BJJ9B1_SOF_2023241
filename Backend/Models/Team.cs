namespace Backend.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public int Chemistry { get; set; }
        public int Rating { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public Player Top { get; set; }
        public Player Jungle { get; set; }
        public Player Mid { get; set; }
        public Player ADC { get; set; }
        public Player Support { get; set; }
        public Match[] MatchHistory { get; set; }
        public Team()
        {
        }
    }
}