using System.Collections.Generic;
using WebDiary.DAL.Entities;
using WebDiary.DAL.PaginationClasses;

namespace WebDiary.ViewModels.NoteViewModels
{
    public class ShowNotesViewModel
    {
        public IEnumerable<Note> Notes { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}