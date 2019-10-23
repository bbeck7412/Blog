namespace Blog.Migrations
{
    using Blog.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Blog.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Blog.Models.ApplicationDbContext context)
        {
            #region Role Creation
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Moderator"))
            {
                roleManager.Create(new IdentityRole { Name = "Moderator" });
            }

            if (!context.Roles.Any(r => r.Name == "Authenticated User"))
            {
                roleManager.Create(new IdentityRole { Name = "Authenticated User" });
            }

            if (!context.Roles.Any(r => r.Name == "Anonymous user"))
            {
                roleManager.Create(new IdentityRole { Name = "Anonymous user" });
            }
            #endregion
            #region User creation
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "Bbeck7412@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Bbeck7412@gmail.com",
                    Email = "Bbeck7412@gmail.com",
                    FirstName = "Brandon",
                    LastName = "Beck",
                    DisplayName = "F7"
                }, "Abc&123");
            }
            if (!context.Users.Any(u => u.Email == "moderator@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "moderator@coderfoundry.com",
                    Email = "moderator@coderfoundry.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    DisplayName = "EpicMod"
                }, "Password1");
            }
            #endregion

            #region Assign roles to Users

            var adminId = userManager.FindByEmail("Bbeck7412@gmail.com").Id;
            userManager.AddToRole(adminId, "Admin");

            var moderatorId = userManager.FindByEmail("moderator@coderfoundry.com").Id;
            userManager.AddToRole(moderatorId, "Moderator");


            #endregion



            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
