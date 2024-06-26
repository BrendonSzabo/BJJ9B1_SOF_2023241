using Backend.Data;
using Backend.Logic;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace Backend.Controllers
{
    public class AdminController : Controller
    {
        private readonly HomeLogic _homeLogic;
        private readonly ModelLogic<User> _userLogic;
        private readonly ModelLogic<Player> _playerLogic;
        private readonly ModelLogic<Team> _teamLogic;
        private readonly ModelLogic<Match> _matchLogic;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public AdminController(HomeLogic homeLogic, ModelLogic<User> userLogic, ModelLogic<Player> playerLogic, ModelLogic<Team> teamLogic, ModelLogic<Match> matchLogic, ApplicationDbContext context, UserManager<User> userManager)
        {
            _homeLogic = homeLogic;
            _userLogic = userLogic;
            _playerLogic = playerLogic;
            _teamLogic = teamLogic;
            _matchLogic = matchLogic;
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            ViewBag.User = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name).Id;
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddMatch(string userId)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == userId);
                if(user.Team.Matches.Count() != 0)
                {
                    user.Team.Matches = new List<Match>();
                }
                if (user is not null)
                {
                    var opponent = _context.Users.FirstOrDefault(x => x.UserName.Contains("asd"));
                    Match match = new Match()
                    {
                        Teams = new List<Team>() { user.Team, opponent.Team },
                        WinnerId = user.Team.Id,
                        LoserId = opponent.Team.Id,
                        WinnerCredits = 1000000,
                        LoserCredits = 0,
                        Team1Score = 3,
                        Team2Score = 2,
                        IsDone = true
                    };
                    Console.WriteLine(match);
                    if (user.Team.Name == string.Empty)
                    {
                        user.Team.Name = "Team" + user.Team.Id;
                    }
                    user.Team.Matches.Add(match);
                    _context.Matches.Add(match);
                    _context.Teams.Update(user.Team);
                    _context.Users.Update(user);
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Error", "Home");
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine($"Exception: {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
