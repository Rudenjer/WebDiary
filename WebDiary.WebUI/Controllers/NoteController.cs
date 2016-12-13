using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebDiary.BLL.Filters;
using WebDiary.BLL.Interfaces;
using WebDiary.DAL.Entities;
using WebDiary.DAL.PaginationClasses;
using WebDiary.DAL.PaginationClasses.Enum;
using WebDiary.ViewModels.NoteViewModels;


namespace WebDiary.Controllers
{
    public class NoteController : Controller
    {
        private readonly INoteService _noteService;
        private readonly ITagService _tagService;
        private readonly IUserService _userService;
        private readonly IRequestFriendService _friendService;

        public NoteController(INoteService noteService, ITagService tagService, IUserService userService, IRequestFriendService friendService)
        {
            _noteService = noteService;
            _tagService = tagService;
            _userService = userService;
            _friendService = friendService;
        }


        [HttpGet]
        public ActionResult Feed(ShowNotesViewModel showNotesViewModel)
        {



            List<Note> friendNotes = new List<Note>();

            if(!User.Identity.IsAuthenticated)
                friendNotes.AddRange(_noteService.GetAllNotes());
            else
            {
                var friends = _friendService.GetAllFriends(User.Identity.GetUserId());

                foreach (var item in friends)
                {
                    friendNotes.AddRange(item.Notes);
                    //showNotesViewModel.Notes.Add(item.Notes);
                }
            }

            

            //showNotesViewModel.Notes = friendNotes;

           // return View(showNotesViewModel);

            if (showNotesViewModel.Notes == null && showNotesViewModel.PageInfo == null)
            {
                showNotesViewModel = new ShowNotesViewModel
                {

                    Notes = friendNotes
                };
            }
            InitializeUser(showNotesViewModel);
            showNotesViewModel.PageInfo.TotalItems = friendNotes.Count;
            if (ModelState.IsValid)
            {
                var filters = new FilterSet
                {
                    PageInfo = showNotesViewModel.PageInfo
                };
                showNotesViewModel.Notes = _noteService.GetNotes(friendNotes, filters);
                return View(showNotesViewModel);
            }

            showNotesViewModel.Notes = friendNotes;

            return View(showNotesViewModel);
        }

        [HttpGet]
        public ActionResult Index(ShowNotesViewModel showNotesViewModel)
        {
            if (showNotesViewModel.Notes == null && showNotesViewModel.PageInfo == null)
            {
                showNotesViewModel = new ShowNotesViewModel
                {

                    Notes = _noteService.GetNotesForUserWithoutFilter(User.Identity.GetUserId())
                };
            }
            InitializeUser(showNotesViewModel);
            showNotesViewModel.PageInfo.TotalItems = _noteService.GetNotesForUserWithoutFilter(User.Identity.GetUserId()).Count();
            if (ModelState.IsValid)
            {
                var filters = new FilterSet
                {
                    PageInfo = showNotesViewModel.PageInfo
                };
                showNotesViewModel.Notes = _noteService.GetNotesForUser(User.Identity.GetUserId(), filters);
                return View(showNotesViewModel);
            }

            showNotesViewModel.Notes = _noteService.GetNotesForUserWithoutFilter(User.Identity.GetUserId());

            return View(showNotesViewModel);
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

        public void InitializeUser(ShowNotesViewModel showNotesViewModel)
        {
            if (User.Identity.GetUserId() != null)
            {
                var user = _userService.GetUserById(User.Identity.GetUserId());
                user.Notes = _noteService.GetNotesForUserWithoutFilter(User.Identity.GetUserId());
            }
            showNotesViewModel.PageInfo = showNotesViewModel.PageInfo ?? new PageInfo
            {
                PageSize = PageSizeEnum.Five,
                PageNumber = 1
            };
            if (User.Identity.GetUserId() == null)
            {
                showNotesViewModel.PageInfo.TotalItems =
                    _noteService.GetNotesForUserWithoutFilter(User.Identity.GetUserId()).Count();
            }
            else
            {
                showNotesViewModel.PageInfo.TotalItems = _userService.CountNotes(User.Identity.GetUserId());
            }
        }

        public ActionResult Note(int id)
        {
            return  View(_noteService.GetNoteById(id));
        }
    }
}