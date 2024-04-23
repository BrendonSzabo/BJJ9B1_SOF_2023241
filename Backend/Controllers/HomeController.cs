﻿using Backend.Data;
using Backend.Logic;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace Backend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly HomeLogic _homeLogic;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<User> userManager, IEmailSender emailSender, HomeLogic homeLogic)
        {
            _emailSender = emailSender;
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _homeLogic = homeLogic;
        }

        //public async Task<IActionResult> Index()
        //{
        //    // var userId = _userManager.GetUserId(User);
        //    var currentUser = _context.Users.FirstOrDefault();
        //    var players = await _context.Players.Include(p => p.Team).ToListAsync();
        //    var model = new Tuple<List<Player>, User>(players, currentUser);
        //    return View("~/Views/Home/Index.cshtml", model);
        //}

        public async Task<IActionResult> DelegateAdmin()
        {
            var success = await _homeLogic.DelegateAdmin(this);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Users()
        {
            return View(_homeLogic.GetUsers());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveAdmin(string uid)
        {
            //var user = _userManager.Users.FirstOrDefault(t => t.Id == uid);
            //await _userManager.RemoveFromRoleAsync(user, "Admin");
            //return RedirectToAction(nameof(Users));
            var success = await _homeLogic.RemoveRole(uid, "Admin");
            if (success)
            {
                return RedirectToAction(nameof(Users));
            }
            else
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GrantAdmin(string uid)
        {
            //var user = _userManager.Users.FirstOrDefault(t => t.Id == uid);
            //await _userManager.AddToRoleAsync(user, "Admin");
            //return RedirectToAction(nameof(Users));
            var success = await _homeLogic.GrantRole(uid, "Admin");
            if (success)
            {
                return RedirectToAction(nameof(Users));
            }
            else
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemovePlayer(string uid)
        {
            //var user = _userManager.Users.FirstOrDefault(t => t.Id == uid);
            //await _userManager.RemoveFromRoleAsync(user, "Player");
            //return RedirectToAction(nameof(Users));
            var success = await _homeLogic.RemoveRole(uid, "Player");
            if (success)
            {
                return RedirectToAction(nameof(Users));
            }
            else
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GrantPlayer(string uid)
        {
            //var user = _userManager.Users.FirstOrDefault(t => t.Id == uid);
            //await _userManager.AddToRoleAsync(user, "Player");
            //return RedirectToAction(nameof(Users));
            var success = await _homeLogic.GrantRole(uid, "Player");
            if (success)
            {
                return RedirectToAction(nameof(Users));
            }
            else
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
        [Authorize]
        public async Task<IActionResult> Privacy()
        {
            var principal = this.User;
            var user = await _homeLogic.Privacy(this);
            return View();
        }

        public async Task<IActionResult> GetImage(string userid)
        {
            //var user = _userManager.Users.FirstOrDefault(t => t.Id == userid);
            //return new FileContentResult(user.Data, user.ContentType);
            var result = await _homeLogic.GetImage(userid);
            return result;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
