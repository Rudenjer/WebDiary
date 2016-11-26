using System.Collections.Generic;
using System.Linq;
using WebDiary.BLL.Filters;
using WebDiary.BLL.Interfaces;
using WebDiary.DAL.Entities;
using WebDiary.DAL.Pipeline;
using WebDiary.DAL.Repository.Interfaces;

namespace WebDiary.BLL.Services
{
    public class NoteSevrice: INoteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPipeline<Note> _pipeline;

        public NoteSevrice(IUnitOfWork iUnitOfWork, IPipeline<Note> pipeline)
        {
            _unitOfWork = iUnitOfWork;
            _pipeline = pipeline;
        }

        public IEnumerable<Note> GetNotesForUser(string userId, FilterSet filters)
        {
            _pipeline.Register(new FilterForGamePaged(filters.PageInfo));

            return _unitOfWork.NoteRepository.Get(m => m.UserId == userId && !m.IsDeleted);
        }

        public void AddNote(Note note, string[] tags)
        {
            SetTagsForNote(note, tags);
            _unitOfWork.NoteRepository.Create(note);
            _unitOfWork.Save();
        }

        public Note GetNoteById(int id)
        {
            return _unitOfWork.NoteRepository.Get(id);
        }

        public void NoteUpdate(Note note, string [] tags=null)
        {
            if (tags != null)
            {
                note.Tags.Clear();
                SetTagsForNote(note, tags);
            }
            _unitOfWork.NoteRepository.Update(note);
            _unitOfWork.Save();
        }

        public void DeleteNote(Note note)
        {
            _unitOfWork.NoteRepository.Delete(note);
            _unitOfWork.Save();
        }

        public int CountNotes(string id)
        {
            return _unitOfWork.NoteRepository.Get(n => n.UserId == id).Count();
        }

        private void SetTagsForNote(Note note, string[] tags)
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
