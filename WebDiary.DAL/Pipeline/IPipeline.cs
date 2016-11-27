using System.Linq;
using WebDiary.DAL.Repository.Interfaces;

namespace WebDiary.DAL.Pipeline
{
    public interface IPipeline<T>
    {
        void Register(IFilter<T> filter);

        IQueryable<T> Process(IQueryable<T> entities);
    }
}
