using System.Collections.Generic;
using WebDiary.DAL.Entities;

namespace WebDiary.BLL.Interfaces
{
    public interface IRequestFriendService
    {
        IEnumerable<ApplicationUser> GetAllFriends(string id);

        IEnumerable<ApplicationUser> GetAllFriendsNotConfirmed(string id);

        void AddFriend(string idUser, string idFriend);

        void ConfirmRequest(int id);

        RequestFriend FindRequest(string userId, string friendId);

        IEnumerable<RequestFriend> GetAllFriends();
    }
}
