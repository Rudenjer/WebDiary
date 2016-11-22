using System.Collections.Generic;
using System.Data.Common;
using System.IO.Ports;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebDiary.DAL.Context;
using WebDiary.DAL.Entities;

namespace WebDiary.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WebDiary.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };

            roleManager.Create(role1);
            roleManager.Create(role2);

            var admin = new ApplicationUser { Email = "denjers@yandex.ru", UserName = "denjers@yandex.ru" };
            string password = "1997Totti1997";
            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
            }

            //List<Tag> TagList = new List<Tag>();

            //TagList.Add(new Tag(Name="gfhgffh"));

            var sport = default(Tag);
            var foot = default(Tag);
            var basket = default(Tag);



            //context.Set<Tag>().AddOrUpdate(d=>d.Name,
            //    sport=new Tag() { Name = "Спорт" },
            //    foot=new Tag() { Name = "Футбол"},
            //    basket = new Tag() { Name = "Баскет" }
            //    );

            context.Notes.AddOrUpdate( new Note[]
            {
                new Note()
                {
                    Name = "Football",
                    Date = new DateTime(2016,11,15,18,34,43),
                    Message = "Football is sucks",
                    Privacy = true,
                    User = userManager.Users.First(u => u.Email == admin.Email),
                    Tags = new List<Tag>() { new Tag() {Name = "Спорт"}, new Tag() {Name = "Футбол" } }

        },
                new Note()
                {
                    Name = "Basketball",
                    Date = new DateTime(2016,11,15,18,34,43),
                    Message = "Basketball is for faggots",
                    Privacy = true,
                    User = userManager.Users.First(u => u.Email == admin.Email),
                    Tags = new List<Tag>() { new Tag() {Name = "Спорт"}, new Tag() {Name = "Баксетбол" } }
                }
            });




            context.SaveChanges();
        }
    }
}
