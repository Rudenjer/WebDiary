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
    }
}
