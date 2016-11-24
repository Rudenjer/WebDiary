using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebDiary.BLL.Interfaces;
using System.ComponentModel.DataAnnotations;
using WebDiary.DAL.Entities;
using WebDiary.ViewModels.NoteViewModels;


namespace WebDiary.Controllers
{
    public class NoteController : Controller
    {



        private readonly INoteService _noteService;
        private readonly IUserService _userService;

        public NoteController(INoteService noteService, IUserService userService)
        {
            _noteService = noteService;
            _userService = userService;
        }


        // GET: Note
        public ActionResult Index()
        {
            var NoteList = _noteService.GetNotesForUser(User.Identity.GetUserId());

            return View(NoteList);
        }

        [HttpGet]
        public ActionResult AddNote()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNote(AddNoteViewModel addNote)
        {
            if (ModelState.IsValid)
            {

                string[] tags = addNote.TagsString.Split(' ');

                List<Tag> Tlist = new List<Tag>();

                foreach (var item in tags)
                {
                    Tlist.Add(new Tag() {Name = item});
                }

                Note note = new Note()
                {
                    Date = DateTime.UtcNow,
                    //User = _userService.GetUserById(User.Identity.GetUserId()),
                    Name = addNote.Name,
                    Message = addNote.Message,
                    UserId = User.Identity.GetUserId(),
                    Privacy = addNote.Privacy,
                    Tags = Tlist
                };

                _noteService.AddNote(note);

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

       
        [HttpGet]
        public ActionResult NoteUpdate(int id)
        {
            var Note = _noteService.GetNoteById(id);

            string tags = "";

            foreach (var item in Note.Tags)
            {
                tags += item.Name.ToString() + " ";
            }

            NoteUpdateViewModel NoteUpdateViewModel= new NoteUpdateViewModel() {Id = id, Name = Note.Name, Message = Note.Message, Privacy = Note.Privacy, TagsString = tags};

            return View(NoteUpdateViewModel);


        }

        [HttpPost]
        public ActionResult NoteUpdate(NoteUpdateViewModel noteUpdateViewModel)
        {

            if (ModelState.IsValid)
            {

                var Note = _noteService.GetNoteById(noteUpdateViewModel.Id);

                string[] tags = noteUpdateViewModel.TagsString.Split(' ');

                List<Tag> Tlist = new List<Tag>();

                foreach (var item in tags)
                {
                    if (item != "")
                    {
                        Tlist.Add(new Tag() {Name = item});
                    }
                }


                Note.Message = noteUpdateViewModel.Message;
                Note.Name = noteUpdateViewModel.Name;
                Note.Tags = Tlist;
                Note.Privacy = noteUpdateViewModel.Privacy;

                _noteService.NoteUpdate(Note);

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public ActionResult DeleteNote(int id)
        {
            var Note = _noteService.GetNoteById(id);

            return View(Note);
        }

        [HttpPost]
        public ActionResult DeleteNote(Note note)
        {
            var Note = _noteService.GetNoteById(note.Id);
            _noteService.DeleteNote(Note);

            return RedirectToAction("Index");
        }



    }
}