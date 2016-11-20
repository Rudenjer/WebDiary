using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebDiary.BLL.Interfaces;


namespace WebDiary.Controllers
{
    public class NoteController : Controller
    {

        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }
        
        
        // GET: Note
        public ActionResult Index()
        {
            var NoteList = _noteService.GetNotesForUser(User.Identity.GetUserId());

            return View(NoteList);
        }
    }
}