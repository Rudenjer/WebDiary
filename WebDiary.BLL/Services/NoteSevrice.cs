using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDiary.BLL.Interfaces;
using WebDiary.DAL.Entities;
using WebDiary.DAL.Repository.Interfaces;

namespace WebDiary.BLL.Services
{
    public class NoteSevrice: INoteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NoteSevrice(IUnitOfWork iUnitOfWork)
        {
            _unitOfWork = iUnitOfWork;
        }

        public IEnumerable<Note> GetNotesForUser(string userId)
        {
            return _unitOfWork.NoteRepository.Get(m => m.UserId == userId);
        } 
    }
}
