using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Logic
{
    public class HomeLogic
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public HomeLogic(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> GrantRole(string uid, string role)
        {
            var user = await _userManager.FindByIdAsync(uid);
            if (user == null)
            {
                return false;
            }
            await _userManager.AddToRoleAsync(user, role);
            return true;
        }

        public async Task<bool> RemoveRole(string uid, string role)
        {
            var user = await _userManager.FindByIdAsync(uid);
            if (user == null)
            {
                return false;
            }
            await _userManager.RemoveFromRoleAsync(user, role);
            return true;
        }

        public async Task<FileContentResult> GetImage(string uid)
        {
            var user = await _userManager.FindByIdAsync(uid);
            return new FileContentResult(user.Data, user.ContentType);
        }

        public async Task<bool> DelegateAdmin(ControllerBase controller)
        {
            var principal = controller.User;
            var user = await _userManager.GetUserAsync(principal);
            var role = new IdentityRole()
            {
                Name = "Admin"
            };
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(role);
            }
            await _userManager.AddToRoleAsync(user, "Admin");
            return true;
        }

        public IEnumerable<User> GetUsers()
        {
            var users = _userManager.Users;
            return users;
        }

        public async Task<User> Privacy(ControllerBase controller)
        {
            var principal = controller.User;
            var user = await _userManager.GetUserAsync(principal);
            return user;
        }
    }
}
