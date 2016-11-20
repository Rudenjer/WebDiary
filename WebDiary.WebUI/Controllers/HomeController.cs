using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebDiary.BLL.Interfaces;

namespace WebDiary.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            var user = _userService.GetUserById(User.Identity.GetUserId());
            return View(user);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.USERID = User.Identity.GetUserId();
            var user=_userService.GetUserById(User.Identity.GetUserId());

            return View(user);
        }
    }
}