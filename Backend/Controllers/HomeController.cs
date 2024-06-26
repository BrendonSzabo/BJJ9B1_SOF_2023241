using Backend.Data;
using Backend.Logic;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Azure.Storage.Blobs;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;
using System.Drawing.Drawing2D;
using Microsoft.AspNet.SignalR.Json;
using System.Text;
using Azure.Storage.Blobs.Models;
using System.Numerics;

namespace Backend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly HomeLogic _homeLogic;
        private readonly ModelLogic<User> _userLogic;
        BlobServiceClient blobServiceClient;
        BlobContainerClient blobContainerClient;
        string blobcstr = "DefaultEndpointsProtocol=https;AccountName=lofblob;AccountKey=5lRciqV+JL7ctiVfOzKZ2Bci5BokMpRZR4sNLr2SOxI+Gt+B7vCqcEfcx5gYw9qJoc7kYf2y1kY3+AStptviMw==;EndpointSuffix=core.windows.net";
        string blobcstr2 = "DefaultEndpointsProtocol=https;AccountName=lofblob;AccountKey=1U2oZ5m87FDf4BngtR4qb+H6oQJx2rC8pIKx4bDAu6SteuazwQ9GrzDW0VjcKqilH8U6SHhWIeLc+AStXOBPYA==;EndpointSuffix=core.windows.net";
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<User> userManager, IEmailSender emailSender, HomeLogic homeLogic)
        {
            _emailSender = emailSender;
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _homeLogic = homeLogic;
            blobServiceClient = new BlobServiceClient(blobcstr2);
            blobContainerClient = blobServiceClient.GetBlobContainerClient("blobs");
        }

        [HttpPost]
        public async Task<IActionResult> MatchDetails(int id)
        {
            var match = _context.Matches.FirstOrDefault(m => m.Id == id);
            if (match == null)
            {
                return ErrorWithMessage("Match not found in database.");
            }

            return View(match);


            //BLOBOS VÁLTOZAT!

            //BlobClient blobClient = blobContainerClient.GetBlobClient($"match_{id}");
            //if (await blobClient.ExistsAsync())
            //{
            //    var downloadInfo = await blobClient.DownloadAsync();
            //    using (var streamReader = new StreamReader(downloadInfo.Value.Content))
            //    {
            //        var matchData = await streamReader.ReadToEndAsync();
            //        //var match = JsonSerializer.Deserialize<Match>(matchData);
            //        var match = System.Text.Json.JsonSerializer.Deserialize<Match>(matchData);

            //        return View(match);
            //    }
            //}
            //return Error("Match not found in blob storage.");
        }

        [HttpPost]
        public async Task<IActionResult> PlayerDetails(int id, [FromForm] IFormFile blobUpload)
        {
            var player = _context.Players.FirstOrDefault(m => m.Id == id);
            if (player == null)
            {
                return ErrorWithMessage("Player not found in database.");
            }
            //BlobClient blobClient = blobContainerClient.GetBlobClient($"player_{id}");
            //using (var streamReader = blobUpload.OpenReadStream())
            //{
            //    //new StreamReader(downloadInfo.Value.Content)
            //    //var playerData = await streamReader.ReadToEndAsync();
            //    //var player = System.Text.Json.JsonSerializer.Deserialize<Player>(playerData);
            //    await blobClient.UploadAsync(streamReader, true);

            //}
            //blobClient.SetAccessTier(AccessTier.Cool);

            //_context.Players.Add(player);
            //_context.SaveChanges();

            return View(player);


            //BLOBOS VÁLTOZAT!

            //var blobClient = blobContainerClient.GetBlobClient($"player_{id}");
            ////if (!await blobClient.ExistsAsync())
            ////{
            //    //var downloadInfo = await blobClient.DownloadAsync();
            //    using (var streamReader = blobUpload.OpenReadStream() )
            //    {
            //        //new StreamReader(downloadInfo.Value.Content)
            //        //var playerData = await streamReader.ReadToEndAsync();
            //        //var player = System.Text.Json.JsonSerializer.Deserialize<Player>(playerData);
            //        await blobClient.UploadAsync(streamReader, true);

            //    }
            //    blobClient.SetAccessTier(AccessTier.Cool);

            ////}
            //    var p = _context.Players.FirstOrDefault(p => p.Id == id);

            //    _context.Players.Add(p);
            //    _context.SaveChanges();

            //    return View(p);

            //else
            //{
            //    // Ha nem létezik, akkor létrehozzuk
            //    //var player = _context.Players.FirstOrDefault(p => p.Id == id);
            //    //if (player != null)
            //    //{
            //    //    var playerJson = JsonSerializer.Serialize(player);
            //    //    using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(playerJson)))
            //    //    {
            //    //        await blobClient.UploadAsync(memoryStream, true);
            //    //    }
            //    //    return View(player);
            //    //}
            //    //else
            //    //{
            //    //    return Error("Player not found in database.");
            //    //}
            //}
            //return Error("Player not found in blob storage.");
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

        public IActionResult Dashboard()
        {
            var user = _userManager.GetUserAsync(User).Result;
            ViewBag.User = user;
            ViewBag.Team = user.Team;
            List<Player> shopPlayers = new List<Player>();
            foreach (var player in _context.Players)
            {
                if (user.Team.Players.FirstOrDefault(x => x.Name == player.Name) == null)
                {
                    shopPlayers.Add(player);
                }
            }
            ViewBag.ShopPlayers = shopPlayers;
            return View();
        }

        public IActionResult Profile()
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                if (user == null)
                {
                    Console.WriteLine("User not found");
                    // Handle the case where the user is not found, e.g., redirect to an error page
                }
                else {
                    Console.WriteLine("User found");
                }
                ViewBag.User = user;
                Console.WriteLine("Profile controller called");
                return View("ProfilePage");  // Explicitly specify the view name
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                // Handle the exception, e.g., log it and return an error view
                return View("Error");
            }
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ErrorWithMessage(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
