using System.Collections.Generic;
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

        public ApplicationUser GetUserByName(string name)
        {
            return _unitOfWork.UserRepository.Get(u => u.Email == name).First();
        }

        public IEnumerable<ApplicationUser> SearchUserByEmail(string email)
        {
            return _unitOfWork.UserRepository.Get(u => u.Email.Contains(email));
        }

        public void UserUpdate(ApplicationUser user)
        {
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Save();
        }

    }
}
