using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void AddNote(Note note)
        {
            _unitOfWork.NoteRepository.Create(note);
            _unitOfWork.Save();
        }

        public Note GetNoteById(int Id)
        {
            return _unitOfWork.NoteRepository.Get(Id);
        }

        public void NoteUpdate(Note note)
        {
            _unitOfWork.NoteRepository.Update(note);
            _unitOfWork.Save();
        }

        public void DeleteNote(Note note)
        {
            _unitOfWork.NoteRepository.Delete(note);
            _unitOfWork.Save();
        }

        public void NoteUpdateByTags(Note note, ICollection<Tag> tags)
        {
            note.Tags.Clear();
            note.Tags = tags;
            _unitOfWork.NoteRepository.Update(note);
            _unitOfWork.Save();
        }
    }
   
}
