using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDiary.DAL.Entities;

namespace WebDiary.BLL.Interfaces
{
    public interface INoteService
    {
        IEnumerable<Note> GetNotesForUser(string userId);

        void AddNote(Note note);

        Note GetNoteById(int Id);

        void NoteUpdate(Note note);

        void DeleteNote(Note note);
    }
}
