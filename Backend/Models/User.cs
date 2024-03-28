namespace Backend.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Region { get; set; }
        public int Credits { get; set; }
        public Team Team { get; set; }
        public User()
        {
        }
    }
}
