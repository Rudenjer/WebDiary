using System.Linq;
using WebDiary.BLL.Interfaces;
using WebDiary.DAL.Entities;
using WebDiary.DAL.Repository.Interfaces;

namespace WebDiary.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork iUnitOfWork)
        {
            _unitOfWork = iUnitOfWork;
        }

        public ApplicationUser GetUserById(string id)
        {
            return _unitOfWork.UserRepository.Get(id);
        }

        public int CountNotes(string id)
        {

            return _unitOfWork.UserRepository.Get(id).Notes.Count();
        }

        public void UserUpdate(ApplicationUser user)
        {
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Save();
        }

    }
}
