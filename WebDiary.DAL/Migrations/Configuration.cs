using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebDiary.DAL.Context;
using WebDiary.DAL.Entities;
using WebDiary.DAL.PaginationClasses;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace WebDiary.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WebDiary.Models.ApplicationDbContext";
        }

        //protected override void Seed(ApplicationDbContext context)
        //{
        //    var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
        //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

        //    var role1 = new IdentityRole { Name = "admin" };
        //    var role2 = new IdentityRole { Name = "user" };

        //    roleManager.Create(role1);
        //    roleManager.Create(role2);

        //    var admin = new ApplicationUser { Email = "denjers@yandex.ru", UserName = "denjers@yandex.ru", PageInfo = new PageInfo() };
        //    string password = "1997Totti1997";
        //    var result = userManager.Create(admin, password);

        //    if (result.Succeeded)
        //    {
        //        userManager.AddToRole(admin.Id, role1.Name);
        //        userManager.AddToRole(admin.Id, role2.Name);
        //    }

        //    var sportTag = new Tag { Id = 1, Name = "Sport" };
        //    context.Tags.AddOrUpdate(sportTag);

        //    Comment comment = new Comment()
        //    {
        //        UserId = admin.Id,
        //        NoteId = 1,
        //        Text = "Lelelele",
        //        DateTime = DateTime.UtcNow
        //    };
        //    context.Comments.AddOrUpdate(comment);
        //    context.SaveChanges();

        //    context.Notes.AddOrUpdate(new Note[]
        //    {
        //        new Note()
        //        {
        //            Name = "Football",
        //            Date = new DateTime(2016,11,15,18,34,43),
        //            Message = "Football is sucks",
        //            Privacy = true,
        //            User = userManager.Users.First(u => u.Email == admin.Email),
        //            Tags = new List<Tag>() { sportTag },
        //            Comments = new List<Comment> { comment }

        //    },
        //        new Note()
        //        {
        //            Name = "Basketball",
        //            Date = new DateTime(2016,11,15,18,34,43),
        //            Message = "Basketball is for faggots",
        //            Privacy = true,
        //            User = userManager.Users.First(u => u.Email == admin.Email),
        //            Tags = new List<Tag>() { sportTag }
        //        }
        //    });
        //    context.SaveChanges();
        //}
    }
}
