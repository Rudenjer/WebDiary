using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebDiary.BLL.Interfaces;
using WebDiary.BLL.Filters;
using WebDiary.BLL.Models.Enums;
using WebDiary.BLL.Paginations;
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
            var filter = new FilterSet
            {
                PageInfo = new PageInfo
                {
                    PageSize = PageSizeEnum.Ten,
                    PageNumber = 1,
                    TotalItems = _noteService.CountNotes(User.Identity.GetUserId())
                }
            };
            var noteList = _noteService.GetNotesForUser(User.Identity.GetUserId(), filter);

            return View(noteList);
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

                var note = new Note()
                {
                    Date = DateTime.UtcNow,
                    //User = _userService.GetUserById(User.Identity.GetUserId()),
                    Name = addNote.Name,
                    Message = addNote.Message,
                    UserId = User.Identity.GetUserId(),
                    Privacy = addNote.Privacy,
                };

                _noteService.AddNote(note, tags);

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
            string tagsString = string.Empty;

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
                note.Message = noteUpdateViewModel.Message;
                note.Name = noteUpdateViewModel.Name;
                note.Privacy = noteUpdateViewModel.Privacy;

                _noteService.NoteUpdate(note, tags);

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
            var note = _noteService.GetNoteById(id);

            return View(note);
        }

        [HttpPost]
        public ActionResult DeleteNote(Note note)
        {
            var currentNote = _noteService.GetNoteById(note.Id);
            currentNote.IsDeleted = true;
            _noteService.NoteUpdate(currentNote);

            return RedirectToAction("Index");
        }



    }
}