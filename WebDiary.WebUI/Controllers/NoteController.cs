﻿using System;
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

        public NoteController(INoteService noteService, ITagService tagService, IUserService userService)
        {
            _noteService = noteService;
            _tagService = tagService;
            _userService = userService;
        }
        

        // GET: Note
        public ActionResult Index(string pageNumber = "1")
        {
            var user = _userService.GetUserById(User.Identity.GetUserId());
            user.Notes = _noteService.GetNotesForUserWithoutFilter(User.Identity.GetUserId());
            InitializeUser();
            user.PageInfo.PageNumber = Convert.ToInt32(pageNumber);

            var filter = new FilterSet
            {
                PageInfo = user.PageInfo
            };
            user.Notes = _noteService.GetNotesForUser(User.Identity.GetUserId(), filter);
            return View(user.Notes);
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

        public void InitializeUser()
        {
            var user = _userService.GetUserById(User.Identity.GetUserId());

            if (user.PageInfo.TotalItems == 0 && user.PageInfo.PageSize == 0)
            {
                user.PageInfo = new PageInfo
                {
                    PageSize = PageSizeEnum.Ten,
                    PageNumber = 1,
                    TotalItems = _userService.CountNotes(User.Identity.GetUserId())
                };
            }
            _userService.UserUpdate(user);
        }
    }
}