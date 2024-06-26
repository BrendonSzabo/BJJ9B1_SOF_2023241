using Backend.Data;
using Backend.Logic;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class CoreController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly HomeLogic _homeLogic;

        public CoreController(UserManager<User> userManager, ApplicationDbContext context, HomeLogic homeLogic)
        {
            _userManager = userManager;
            _context = context;
            _homeLogic = homeLogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Buy(string userId, int playerId)
        {
            var player = _context.Players.FirstOrDefault(x => x.Id == playerId);
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (player is not null && user.Credits >= player.Price)
            {
                Player newPlayer = CopyPlayer(player);
                newPlayer.Team = user.Team;
                newPlayer.TeamId = user.Team.Id;
                var newUser = user;
                newUser.Team.Players.Add(newPlayer);
                _context.Users.Update(newUser);
                _context.Teams.Update(newUser.Team);
                _context.SaveChanges();
                return RedirectToAction("Dashboard", "Home");
            }
            return RedirectToAction("Error", "Home");
        }

        public IActionResult AssignPlayer(string userId, int playerId, int role)
        {
            RoleEnum roleEnum = (RoleEnum)role;
            var player = _context.Players.FirstOrDefault(x => x.Id == playerId);
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (player is not null && user.Team.Players.Contains(player))
            {
                player.Role = roleEnum;
                _context.Players.Update(player);
                _context.SaveChanges();
                return RedirectToAction("Dashboard", "Home");
            }
            return RedirectToAction("Error", "Home");
        }

        public Player CopyPlayer(Player player)
        {
            return new Player
            {
                Name = player.Name,
                Image = player.Image,
                Rating = player.Rating,
                Price = player.Price,
                Role = player.Role,
                Region = player.Region,
                Language = player.Language,
                Nationality = player.Nationality,
                YearsAsPro = player.YearsAsPro,
            };
        }
    }
}
