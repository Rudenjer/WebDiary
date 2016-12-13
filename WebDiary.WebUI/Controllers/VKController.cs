using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VkNet;
using VkNet.Enums.Filters;

namespace WebDiary.Controllers
{
    public class VKController : Controller
    {
        private ulong appID = 5774772;
        // GET: VK
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(string Login, string Password)
        {
            var vk = new VkApi();
            
            vk.Authorize(new ApiAuthParams
            {
                ApplicationId = appID,
                Login = Login,
                Password = Password,
                Settings = Settings.All,
                 
            });
            return View();
        }
    }
    
}