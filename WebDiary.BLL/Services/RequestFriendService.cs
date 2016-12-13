using System.Collections.Generic;
using System.Linq;
using WebDiary.BLL.Interfaces;
using WebDiary.DAL.Entities;
using WebDiary.DAL.Repository.Interfaces;

namespace WebDiary.BLL.Services
{
    public class RequestFriendService : IRequestFriendService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RequestFriendService(IUnitOfWork iUnitOfWork)
        {
            _unitOfWork = iUnitOfWork;
        }

        public IEnumerable<ApplicationUser> GetAllFriends(string id)
        {
            var userFriendRelations = _unitOfWork.RequestFriendRepository.Get(f => f.UserId == id);

            return userFriendRelations.Select(friend => _unitOfWork.UserRepository.Get(friend.FriendId)).ToList();
        }

        public void AddFriend(string idUser, string idFriend)
        {
            _unitOfWork.RequestFriendRepository.Create(new RequestFriend
            {
                UserId = idUser,
                FriendId = idFriend,
                Status = "InProgress"
            });
            _unitOfWork.Save();
        }

        public void ConfirmRequest(int id)   //подтверждение
        {
            var request = _unitOfWork.RequestFriendRepository.Get(id);
            request.Status = "Confirmed";
            
            _unitOfWork.RequestFriendRepository.Update(request);
            _unitOfWork.Save();
        }
    }
}
