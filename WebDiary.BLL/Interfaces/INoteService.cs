using System.Collections.Generic;
using WebDiary.BLL.Filters;
using WebDiary.DAL.Entities;

namespace WebDiary.BLL.Interfaces
{
    public interface INoteService
    {
        int CountNotes(string id);

        IEnumerable<Note> GetNotesForUser(string userId, FilterSet filters);

        void AddNote(Note note, string[] tags);

        Note GetNoteById(int id);

        void NoteUpdate(Note note, string[] tags=null);

        void DeleteNote(Note note);
    }
}
