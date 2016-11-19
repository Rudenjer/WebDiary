using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDiary.DAL.Entities;

namespace WebDiary.DAL.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Note, int> NoteRepository { get; }

        IRepository<Tag, int> TagRepository { get; }

        IRepository<ApplicationUser, string> UserRepository { get; }

        void Save();
    }
}
