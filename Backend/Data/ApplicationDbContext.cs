using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>()
                .HasMany(m => m.Teams)
                .WithMany(t => t.Matches)
                .UsingEntity<Dictionary<string, object>>(
                    "MatchTeams",
                    j => j
                        .HasOne<Team>()
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .HasConstraintName("FK_MatchTeams_Teams_TeamId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<Match>()
                        .WithMany()
                        .HasForeignKey("MatchId")
                        .HasConstraintName("FK_MatchTeams_Matches_MatchId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("MatchId", "TeamId");
                        j.ToTable("MatchTeams");
                    });

            modelBuilder.Entity<Team>()
                .HasMany(t => t.Players)
                .WithOne(p => p.Team)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Team>()
                .HasOne(t => t.User)
                .WithOne(u => u.Team) // Specify the navigation property
                .HasForeignKey<Team>(t => t.UserId) // Specify the foreign key property
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Team) // Specify the navigation property
                .WithOne(t => t.User) // Specify the inverse navigation property
                .HasForeignKey<Team>(t => t.UserId) // Specify the foreign key property
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IdentityRole>().HasData(
                new { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new { Id = "2", Name = "Player", NormalizedName = "PLAYER" });

            base.OnModelCreating(modelBuilder);
        }

        //public void SeedData()
        //{
        //    Match match1 = new Match();

        //    //User user1 = new User("user1");
        //    //User user2 = new User("user2");

        //    Team team1 = new Team("Team A", RegionEnum.LEC, 90, 10, 1, null);
        //    Team team2 = new Team("Team B", RegionEnum.LCK, 80, 9, 2, null);

        //    Player player1 = new Player("John Doe", "~/lib/assets/placeholder.jpg", 85, RoleEnum.Top, "English", NationalityEnum.CN, 5, team1.Id);
        //    Player player2 = new Player("Alice Smith", "~/lib/assets/placeholder.jpg", 82, RoleEnum.Jungle, "Spanish", NationalityEnum.CN, 7, team1.Id);
        //    Player player3 = new Player("Michael Johnson", "~/lib/assets/placeholder.jpg", 78, RoleEnum.Mid, "French", NationalityEnum.CN, 6, team1.Id);
        //    Player player4 = new Player("Emily Brown", "~/lib/assets/placeholder.jpg", 80, RoleEnum.ADC, "German", NationalityEnum.CN, 4, team1.Id);
        //    Player player5 = new Player("David Wilson", "~/lib/assets/placeholder.jpg", 83, RoleEnum.Support, "Italian", NationalityEnum.CN, 8, team1.Id);
        //    Player player6 = new Player("Sophia Garcia", "~/lib/assets/placeholder.jpg", 79, RoleEnum.Top, "Portuguese", NationalityEnum.CN, 3, team2.Id);
        //    Player player7 = new Player("James Martinez", "~/lib/assets/placeholder.jpg", 81, RoleEnum.Jungle, "Dutch", NationalityEnum.CN, 9, team2.Id);
        //    Player player8 = new Player("Isabella Rodriguez", "~/lib/assets/placeholder.jpg", 77, RoleEnum.Mid, "Russian", NationalityEnum.CN, 5, team2.Id);
        //    Player player9 = new Player("Daniel Brown", "~/lib/assets/placeholder.jpg", 84, RoleEnum.ADC, "Swedish", NationalityEnum.CN, 6, team2.Id);
        //    Player player10 = new Player("Olivia Lopez", "~/lib/assets/placeholder.jpg", 76, RoleEnum.Support, "Japanese", NationalityEnum.CN, 7, team2.Id);

        //    team1.Players.Add(player1);
        //    team1.Players.Add(player2);
        //    team1.Players.Add(player3);
        //    team1.Players.Add(player4);
        //    team1.Players.Add(player5);
        //    team2.Players.Add(player6);
        //    team2.Players.Add(player7);
        //    team2.Players.Add(player8);
        //    team2.Players.Add(player9);
        //    team2.Players.Add(player10);

        //    team1.TopId = player1.Id;
        //    team1.JungleId = player2.Id;
        //    team1.MidId = player3.Id;
        //    team1.ADCId = player4.Id;
        //    team1.SupportId = player5.Id;
        //    team2.TopId = player6.Id;
        //    team2.JungleId = player7.Id;
        //    team2.MidId = player8.Id;
        //    team2.ADCId = player9.Id;
        //    team2.SupportId = player10.Id;

        //    team1.User = user1;
        //    team2.User = user2;

        //    team1.UserId = user1.Id;
        //    team2.UserId = user2.Id;

        //    user1.TeamId = team1.Id;
        //    user2.TeamId = team2.Id;

        //    user1.Credits = 1000;
        //    user2.Credits = 500;
        //    user1.Team = team1;
        //    user2.Team = team2;

        //    match1.Teams.Add(team1);
        //    match1.Teams.Add(team2);
        //    match1.Date = new DateTime(2021, 10, 10).ToString();
        //    match1.Region = RegionEnum.LCK;
        //    match1.WinnerCredits = 100;
        //    match1.LoserCredits = 50;
        //    match1.WinnerId = team1.Id;
        //    match1.LoserId = team2.Id;
        //    match1.Team1Score = 3;
        //    match1.Team2Score = 2;

        //    Matches.Add(match1);

        //    Users.Add(user1);
        //    Teams.Add(team1);
        //    Users.Add(user2);
        //    Teams.Add(team2);

        //    Players.AddRange(new Player[]
        //    {
        //        player1, player2, player3, player4, player5, player6, player7, player8, player9, player10
        //    });

        //    SaveChanges();
        //}
    }
}
