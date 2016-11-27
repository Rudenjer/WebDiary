using WebDiary.DAL.Entities;

namespace WebDiary.BLL.Interfaces
{
    public interface IUserService
    {
        ApplicationUser GetUserById(string id);
    }
}
