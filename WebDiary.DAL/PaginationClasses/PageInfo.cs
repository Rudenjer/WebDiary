using System;
using WebDiary.DAL.PaginationClasses.Enum;

namespace WebDiary.DAL.PaginationClasses
{
    public class PageInfo
    {
        public int PageNumber { get; set; }

        public PageSizeEnum PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / (int)PageSize);
    }
}
