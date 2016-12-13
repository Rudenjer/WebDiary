using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebDiary.BLL.Interfaces;
using WebDiary.ViewModels.FriendViewModel;

namespace WebDiary.Controllers
{
    public class FriendController : Controller
    {
        private readonly IRequestFriendService _requestFriendService;
        private readonly IUserService _userService;

        public FriendController(IUserService userService, IRequestFriendService requestFriendService)
        {
            _userService = userService;
            _requestFriendService = requestFriendService;
        }

        public ActionResult SearchFriend(string searchQuery)
        {
            var users = _userService.SearchUserByEmail(searchQuery).Where(u => u.Id != User.Identity.GetUserId());

            return View(users);
        }

        public ActionResult Details(string id)
        {
            var user = _userService.GetUserById(id);

            var friend = new FriendDetails
            {
                Name = user.UserName,
                Email = user.Email,
                friendIdsLst = _requestFriendService.GetAllFriends().Where(f => user.Id == f.FriendId).Select(u => u.UserId).ToList()
            };

            return View(friend);
        }

        public ActionResult AddToFriend(string email)
        {
            var user = _userService.GetUserByName(email);

            _requestFriendService.AddFriend(User.Identity.GetUserId(), user.Id);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Requests()
        {
            var userFriendNotConfirmed = _requestFriendService.GetAllFriendsNotConfirmed(User.Identity.GetUserId());

            return View(userFriendNotConfirmed);
        }

        public ActionResult Confirmed(string id)
        {
            var user = _requestFriendService.FindRequest(User.Identity.GetUserId(), id);
            _requestFriendService.ConfirmRequest(user.Id);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Friends()
        {
            return View(_requestFriendService.GetAllFriends(User.Identity.GetUserId()));
        }
    }
}