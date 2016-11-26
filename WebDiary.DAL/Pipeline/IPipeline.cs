using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDiary.DAL.Repository.Interfaces;

namespace WebDiary.DAL.Pipeline
{
    public interface IPipeline<T>
    {
        void Register(IFilter<T> filter);

        IQueryable<T> Process(IQueryable<T> entities);
    }
}
