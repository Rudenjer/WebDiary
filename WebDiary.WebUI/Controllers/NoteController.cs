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
        private readonly ITagService _tagService;

        public NoteController(INoteService noteService, ITagService tagService)
        {
            _noteService = noteService;
            _tagService = tagService;
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
            var note = _noteService.GetNoteById(id);
            var tags = _tagService.GetTagsByNote(note);
            string tagsString = "";

            foreach (var item in tags)
            {
                tagsString += item.Name + " ";
            }

            NoteUpdateViewModel noteUpdateViewModel= new NoteUpdateViewModel() {Id = id, Name = note.Name, Message = note.Message, Privacy = note.Privacy, TagsString = tagsString};

            return View(noteUpdateViewModel);


        }

        [HttpPost]
        public ActionResult NoteUpdate(NoteUpdateViewModel noteUpdateViewModel)
        {

            if (ModelState.IsValid)
            {

                var note = _noteService.GetNoteById(noteUpdateViewModel.Id);

                string[] tags = noteUpdateViewModel.TagsString.Split(' ');

                List<Tag> tlist = new List<Tag>();

                //foreach (var item in tags)
                //{
                //    if (item != string.Empty && _tagService.GetAllTags().FirstOrDefault(t => t.Name == item) == null)
                //    {
                //        _tagService.AddTag(new Tag() {Name=item});

                //        //tlist.Add(new Tag() { Name = item });
                //    }
                //    //tlist.Add(_tagService.GetByName(item));
                //}
                tlist.Add(new Tag() { Name = "Loh" });
                tlist.Add(_tagService.GetByName("Loh"));
                note.Message = noteUpdateViewModel.Message;
                note.Name = noteUpdateViewModel.Name;
                note.Tags = tlist;
                note.Privacy = noteUpdateViewModel.Privacy;

                _noteService.NoteUpdate(note);

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