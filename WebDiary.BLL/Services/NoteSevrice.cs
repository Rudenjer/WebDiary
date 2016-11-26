using System.Collections.Generic;
using System.Linq;
using WebDiary.BLL.Interfaces;
using WebDiary.DAL.Entities;
using WebDiary.DAL.Repository.Interfaces;

namespace WebDiary.BLL.Services
{
    public class NoteSevrice: INoteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NoteSevrice(IUnitOfWork iUnitOfWork)
        {
            _unitOfWork = iUnitOfWork;
        }

        public IEnumerable<Note> GetNotesForUser(string userId)
        {
            return _unitOfWork.NoteRepository.Get(m => m.UserId == userId);
        }

        public void AddNote(Note note, string[] tags)
        {
            SetTagsForNoteCreate(note, tags);
            _unitOfWork.NoteRepository.Create(note);
            _unitOfWork.Save();
        }

        public Note GetNoteById(int id)
        {
            return _unitOfWork.NoteRepository.Get(id);
        }

        public void NoteUpdate(Note note, string [] tags)
        {
            note.Tags.Clear();
            
            SetTagsForNoteCreate(note, tags);
            _unitOfWork.NoteRepository.Update(note);
            _unitOfWork.Save();
        }

        public void DeleteNote(Note note)
        {
            _unitOfWork.NoteRepository.Delete(note);
            _unitOfWork.Save();
        }

        private void SetTagsForNoteUpdate(Note note, string[] tags)
        {
            foreach (var item in tags)
            {
                var tag = new Tag() { Name = item };
                if (item != string.Empty && _unitOfWork.TagRepository.Get().FirstOrDefault(t => t.Name == item) == null)
                {
                    _unitOfWork.TagRepository.Create(tag);
                    _unitOfWork.Save();

                    tag = _unitOfWork.TagRepository.Get(t => t.Name == item).FirstOrDefault();
                    if (tag != null)
                    {
                        note.Tags?.Add(tag);
                    }
                }
            }
        }

        private void SetTagsForNoteCreate(Note note, string[] tags)
        {
            foreach (var item in tags)
            {
                var tag = new Tag() { Name = item };
                if (item != string.Empty && _unitOfWork.TagRepository.Get().FirstOrDefault(t => t.Name == item) == null)
                {
                    _unitOfWork.TagRepository.Create(tag);
                    note.Tags.Add(tag);
                }

                tag = _unitOfWork.TagRepository.Get(t => t.Name == item).FirstOrDefault();
                if (tag != null)
                {
                    note.Tags.Add(tag);
                }
            }
        }
    }
}
