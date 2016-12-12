using WebDiary.DAL.Entities;

namespace WebDiary.BLL.Interfaces
{
    public interface IUserService
    {
        ApplicationUser GetUserById(string id);

        void UserUpdate(ApplicationUser user);

        int CountNotes(string id);

        ApplicationUser GetUserByName(string name);
    }
}
