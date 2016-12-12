using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebDiary.DAL.Entities;

namespace WebDiary.DAL.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Tag> Tags { get; set; }

        public DbSet<Note> Notes { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<RequestFriend> RequestFriends { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}