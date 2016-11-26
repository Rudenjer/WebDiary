using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDiary.BLL.Paginations;
using WebDiary.DAL.Entities;
using WebDiary.DAL.Repository.Interfaces;

namespace WebDiary.BLL.Filters
{
    public class FilterForGamePaged : IFilter<Note>
    {
        public FilterForGamePaged(PageInfo pageInfo)
        {
            PageInfo = pageInfo;
        }

        private PageInfo PageInfo { get; }

        public IQueryable<Note> Execute(IQueryable<Note> games)
        {
            return games.Skip((PageInfo.PageNumber - 1) * (int)PageInfo.PageSize)
                    .Take((int)PageInfo.PageSize);
        }
    }
}
