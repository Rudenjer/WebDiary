using System.ComponentModel.DataAnnotations;

namespace WebDiary.DAL.Entities
{
    public class RequestFriend
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public string FriendId { get; set; }

        public string Status { get; set; }
    }
}
