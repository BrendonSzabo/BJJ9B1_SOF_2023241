using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Analysis;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public ApiController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpPost]
        [Route("ListUnfinishedMatches")]
        public IActionResult ListUnfinishedMatches()
        {
            try
            {
                List<Match> matches = _context.Matches.Where(x => x.IsDone == false).ToList();
                if (matches.Count() == 0)
                {
                    return Ok(new { message = "NoContent matches" });
                }
                return Ok(matches);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("ListFinishedMatches")]
        public IActionResult ListFinishedMatches()
        {
            try
            {
                List<Match> matches = _context.Matches.Where(x => x.IsDone == true).ToList();
                if (matches.Count() == 0)
                {
                    return Ok(new { message = "NoContent matches" });
                }
                return Ok(matches);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("ListPlayerByRating")]
        public IActionResult ListPlayerByRating(int lower, int? higher)
        {
            try
            {
                if (lower <= 0)
                {
                    lower = 0;
                }
                if (higher is null || lower > higher)
                {
                    higher = 100;
                }
                List<Player> result = _context.Players.Where(x => x.Rating >= lower && x.Rating <= higher).ToList();
                if (result.Count() == 0)
                {
                    return Ok(new { message = "NoContent players" });
                }
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("ListUserByRating")]
        [Authorize(Roles = "Admin")]
        public IActionResult ListUserByRating(int lower, int? higher)
        {
            try
            {
                if (lower <= 0)
                {
                    lower = 0;
                }
                if (higher is null || lower > higher)
                {
                    higher = 100;
                }
                List<User> result = _context.Users.Where(x => x.Rating >= lower && x.Rating <= higher).ToList();
                if (result.Count() == 0)
                {
                    return Ok(new { message = "NoContent players" });
                }
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("ListUsers")]
        [Authorize(Roles = "Admin")]
        public IActionResult ListUsers()
        {
            try
            {
                return Ok(_context.Users);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("ListTeamByRating")]
        public IActionResult ListTeamByRating(int lower, int? higher)
        {
            try
            {
                if (lower <= 0)
                {
                    lower = 0;
                }
                if (higher is null || lower > higher)
                {
                    higher = 100;
                }
                return Ok(_context.Teams.Where(x => x.Rating >= lower && x.Rating <= higher));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("GetTeamWithPlayer")]
        public IActionResult GetTeamWithPlayer(string name)
        {
            try
            {
                var player = _context.Players.FirstOrDefault(x => x.Name == name);
                if (player is null)
                {
                    return BadRequest("Player not found in database.");
                }
                return Ok(_context.Teams.Where(x => x.Players.Contains(player)));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("SeedData")]
        [Authorize(Roles = "Admin")]
        public IActionResult SeedData()
        {
            Console.WriteLine("Seeding data");
            if (_context.Players is not null || _context.Players.Count() == 0)
            {
                try
                {
                    var df = DataFrame.LoadCsv(@"C:\Users\lordn\Desktop\Szerveroldali feleves\Backend\Backend\playerdata.csv");
                    foreach (var row in df.Rows)
                    {
                        Player player = new Player();
                        player.Name = row["Player"].ToString();
                        if (Enum.TryParse<NationalityEnum>(row["Nationality"].ToString(), out NationalityEnum nationality))
                        {
                            player.Nationality = nationality;
                        }
                        else
                        {
                            player.Nationality = NationalityEnum.Unknown;
                        }
                        player.Rating = int.Parse(row["Rating"].ToString());
                        player.Image = row["Image"].ToString();
                        _context.Players.Add(player);
                    }
                    _context.SaveChanges();
                    return Ok(new { message = "Done" });
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return Ok(new { message = "Error when seeding. Either there is data or the table doesnt exist." });
        }

        [HttpPost]
        [Route("ListPlayers")]
        public IActionResult ListPlayers()
        {
            try
            {
                return Ok(_context.Players);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}