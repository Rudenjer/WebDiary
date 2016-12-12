using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebDiary.DAL.PaginationClasses;

namespace WebDiary.DAL.Entities
{

    public class ApplicationUser : IdentityUser
    {
        public PageInfo PageInfo { get; set; }

        public virtual IEnumerable<Note> Notes { get; set; } = new List<Note>();

        public virtual IEnumerable<Comment> Comments { get; set; } = new List<Comment>(); 

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}
