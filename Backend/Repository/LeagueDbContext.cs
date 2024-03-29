using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class LeagueDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }

        public LeagueDbContext()
        {
            this.Database.EnsureCreated();
        }

        public LeagueDbContext(DbContextOptions<LeagueDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder
                    .UseInMemoryDatabase("LeagueDatabase")
                    .UseLazyLoadingProxies();
            }

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>(match => match
            .HasMany(match => match.Teams)
            .WithMany(team => team.MatchHistory)
            .UsingEntity<MatchTeam>());

            modelBuilder.Entity<Team>(team => team
            .HasMany(team => team.Players)
            .WithOne(player => player.Team)
            .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<User>(user => user
            .HasOne(user => user.Team)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade));


            modelBuilder.Entity<Team>().HasData(new Team[]
            {

            });
        }
    }
}
