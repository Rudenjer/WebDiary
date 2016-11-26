using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDiary.DAL.Repository.Interfaces
{
    public interface IFilter<TKey>
    {
        IQueryable<TKey> Execute(IQueryable<TKey> games);
    }
}
