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
            var users = _userService.SearchUserByEmail(searchQuery);

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
    }
}