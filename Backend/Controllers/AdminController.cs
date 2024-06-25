using Backend.Logic;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class AdminController : Controller
    {
        private readonly HomeLogic _homeLogic;
        private readonly ModelLogic<User> _userLogic;
        private readonly ModelLogic<Player> _playerLogic;
        private readonly ModelLogic<Team> _teamLogic;
        private readonly ModelLogic<Match> _matchLogic;

        public AdminController(HomeLogic homeLogic, ModelLogic<User> userLogic, ModelLogic<Player> playerLogic, ModelLogic<Team> teamLogic, ModelLogic<Match> matchLogic)
        {
            _homeLogic = homeLogic;
            _userLogic = userLogic;
            _playerLogic = playerLogic;
            _teamLogic = teamLogic;
            _matchLogic = matchLogic;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
