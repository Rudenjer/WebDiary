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
