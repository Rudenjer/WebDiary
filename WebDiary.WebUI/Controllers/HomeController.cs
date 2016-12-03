using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebDiary.BLL.Interfaces;

namespace WebDiary.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly INoteService _noteService;

        public HomeController(IUserService userService, INoteService noteService)
        {
            _userService = userService;
            _noteService = noteService;
        }

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Note");
            }
            var model = _noteService.GetAllPublicNotes();
            return View(model);
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