using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDiary.DAL.Repository.Interfaces;

namespace WebDiary.DAL.Pipeline
{
    public class Pipeline<T> : IPipeline<T>
    {
        private readonly IList<IFilter<T>> _filters = new List<IFilter<T>>();

        public void Register(IFilter<T> filter)
        {
            _filters.Add(filter);
        }

        public IQueryable<T> Process(IQueryable<T> entities)
        {
            return _filters.Aggregate(entities, (current, filter) => filter.Execute(current));
        }
    }
}
