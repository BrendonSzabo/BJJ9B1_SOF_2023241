using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using Backend.Data.Repository;
using Backend.Models;
using Backend.Data;

namespace Backend.Controllers
{
    public class ModelController<T> : Controller where T : class, IModelBase
    {
        private readonly ModelLogic<T> _modelLogic;

        public ModelController(ModelLogic<T> logic)
        {
            _modelLogic = logic;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View($"../{nameof(T)}/List{nameof(T)}", _modelLogic.GetItems());
        }

        [Authorize(Roles = "User,Admin")]
        public IActionResult ListItems()
        {
            return View(_modelLogic.GetItems());
        }

        [Authorize(Roles = "User,Admin")]
        public IActionResult AddItem()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> AddItem(T item)
        {
            /*meeting.TeamPrincipalId = _userManager.GetUserId(this.User);
            var old = _db.Meetings.FirstOrDefault(t => t.Name == meeting.Name && t.TeamPrincipalId == meeting.TeamPrincipalId);
            if (old == null)
            {
                _db.Meetings.Add(meeting);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Item created successfully!";
                return RedirectToAction(nameof(ListMeetings));
            }
            else
            {

                TempData["WarningMessage"] = "Item already exist!";
                return RedirectToAction(nameof(AddMeeting));
            }*/
            ;
            var succes = await _modelLogic.AddItem(item);
            if (succes)
            {
                TempData["SuccessMessage"] = "Item created successfully!";
                return RedirectToAction(nameof(ListItems));
            }
            else
            {
                TempData["WarningMessage"] = "Item already exist!";
                return RedirectToAction(nameof(ListItems));
            }
        }

        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> DeleteItem(string uid)
        {
            /*var item = _db.Meetings.FirstOrDefault(t => t.Uid == uid);
            if (item != null && item.TeamPrincipalId == _userManager.GetUserId(this.User))
            {
                _db.Meetings.Remove(item);
                _db.SaveChanges();
            }
            TempData["DeleteSuccessMessage"] = "Item deleted successfully!";
            return RedirectToAction(nameof(ListMeetings));*/

            var succes = await _modelLogic.RemoveItem(int.Parse(uid));
            if (succes)
            {
                TempData["DeleteSuccessMessage"] = "Item deleted successfully!";
                return RedirectToAction(nameof(ListItems));
            }
            else
            {
                TempData["DeleteSuccessMessage"] = "Item deleted unsuccessfully!";
                return RedirectToAction(nameof(ListItems));
            }

        }

        [HttpPost]
        public IActionResult ResetSuccessMessage()
        {
            TempData["SuccessMessage"] = null;
            return Ok();
        }

        [HttpPost]
        public IActionResult ResetDeleteSuccessMessage()
        {
            TempData["DeleteSuccessMessage"] = null;
            return Ok();
        }

        [HttpPost]
        public IActionResult ResetWarningMessage()
        {
            TempData["WarningMessage"] = null;
            return Ok();
        }
    }
}
