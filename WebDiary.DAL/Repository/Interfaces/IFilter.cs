using System.Linq;

namespace WebDiary.DAL.Repository.Interfaces
{
    public interface IFilter<TKey>
    {
        IQueryable<TKey> Execute(IQueryable<TKey> notes);
    }
}
