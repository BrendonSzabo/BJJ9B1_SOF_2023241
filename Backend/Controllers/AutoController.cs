using Backend.Data;
using Backend.Logic;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly HomeLogic _homeLogic;
        private readonly ModelLogic<User> _userLogic;

        public AutoController()
        {
        }

        [HttpPost]
        [Route("SetMatch")]
        public IActionResult SetMatch(User user)
        {
            User opponent = _context.Users.FirstOrDefault(x => x.Id != user.Id && x.Team.HasActiveMatch == false && x.Rating >= user.Rating-5 && x.Rating <= user.Rating+5);
            if (opponent is not null)
            {
                var result = CreateMatch(user, opponent);
                if (result.done is false)
                {
                    return BadRequest(result.e);
                }
                return Ok();
            }
            return BadRequest("No opponent found.");
        }

        [HttpPost]
        [Route("SetMatches")]
        public IActionResult SetMatches()
        {
            foreach (var user in _context.Users)
            {
                User opponent = _context.Users.FirstOrDefault(x => x.Id != user.Id && x.Team.HasActiveMatch == false && x.Rating >= user.Rating - 5 && x.Rating <= user.Rating + 5 && x.Team.Region == user.Team.Region);
                if (opponent is not null)
                {
                    var result = CreateMatch(user, opponent);
                    if (result.done is false)
                    {
                        return BadRequest(result.e);
                    }
                }
            }
            return Ok();
        }

        [HttpPost]
        [Route("PlayMatches")]
        public IActionResult PlayMatches()
        {
            try
            {
                var date = DateTime.Now;
                foreach (var team in _context.Teams)
                {
                    if (team.HasActiveMatch is true)
                    {
                        var match = team.Matches.FirstOrDefault(x => x.IsDone == false);
                        if (match is not null)
                        {
                            Random rnd = new Random();
                            var team1chance = rnd.Next(1, 11) + 2;
                            var team2chance = rnd.Next(1, 11);
                            if (team1chance > team2chance)
                            {
                                var winner = match.Teams.First();
                                var loser = match.Teams.Last();
                                match.WinnerId = winner.Id;
                                match.LoserId = loser.Id;
                                if (winner.Rating >= loser.Rating)
                                {
                                    var differental = loser.Rating - winner.Rating;
                                    match.WinnerCredits = 1000;
                                    match.LoserCredits = 800 - (500 - (differental * 100));
                                }
                                else
                                {
                                    var differental = winner.Rating - loser.Rating;
                                    match.WinnerCredits = 1000 + (differental * 100);
                                    match.LoserCredits = 700 - (differental * 100);
                                }
                            }
                            else
                            {
                                var winner = match.Teams.Last();
                                var loser = match.Teams.First();
                                match.WinnerId = winner.Id;
                                match.LoserId = loser.Id;
                                if (winner.Rating >= loser.Rating)
                                {
                                    var differental = loser.Rating - winner.Rating;
                                    match.WinnerCredits = 1000;
                                    match.LoserCredits = 800 - (500 - (differental * 100));
                                }
                                else
                                {
                                    var differental = winner.Rating - loser.Rating;
                                    match.WinnerCredits = 1000 + (differental * 100);
                                    match.LoserCredits = 700 - (differental * 100);
                                }
                            }
                            match.Date = $"{date.Year}/{date.Month}/{date.Day}";
                            match.IsDone = true;
                            match.Teams.First().HasActiveMatch = false;
                            match.Teams.Last().HasActiveMatch = false;
                            _context.Matches.Update(match);
                            _context.SaveChanges();
                        }
                    }
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private (bool done, string? e) CreateMatch(User user, User opponent)
        {
            try
            {
                Match match = new Match();
                match.Region = user.Team.Region;
                match.Teams = new List<Team>();
                match.Teams.Add(user.Team);
                match.Teams.Add(opponent.Team);
                user.Team.HasActiveMatch = true;
                opponent.Team.HasActiveMatch = true;
                _context.Matches.Add(match);
                user.Team.Matches.Add(match);
                opponent.Team.Matches.Add(match);
                _context.Users.Update(user);
                _context.Users.Update(opponent);
                _context.SaveChanges();
                return (true, string.Empty);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }
    }
}
