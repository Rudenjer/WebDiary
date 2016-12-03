using System.Collections.Generic;
using WebDiary.BLL.Filters;
using WebDiary.DAL.Entities;

namespace WebDiary.BLL.Interfaces
{
    public interface INoteService
    {
        IEnumerable<Note> GetNotesForUser(string userId, FilterSet filters);

        IEnumerable<Note> GetAllPublicNotes();

        void AddNote(Note note, string[] tags);

        Note GetNoteById(int id);

        IEnumerable<Note> GetNotesForUserWithoutFilter(string userId);

        void NoteUpdate(Note note, string[] tags=null);

        void DeleteNote(Note note);
    }
}
