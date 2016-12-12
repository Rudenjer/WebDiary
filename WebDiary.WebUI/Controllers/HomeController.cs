using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebDiary.BLL.Interfaces;

namespace WebDiary.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRequestFriendService _requestFriendService;

        public HomeController(IUserService userService, IRequestFriendService requestFriendService)
        {
            _userService = userService;
            _requestFriendService = requestFriendService;
        }

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                _requestFriendService.AddFriend(User.Identity.GetUserId(), "ae0f3565-7ce0-4b09-bf73-3588a499bbb0");
                var friends = _requestFriendService.GetAllFriends(User.Identity.GetUserId());
                return View("Friends", friends);
            }

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message
                = "Your contact page.";
            ViewBag.USERID
                =
                User.Identity.GetUserId
                    ();

            var user = _userService.GetUserById(User.Identity.GetUserId());
            
            return

                View(user);
        }
    }

}