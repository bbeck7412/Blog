namespace Blog.Migrations
{
    using Blog.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Configuration;

    internal sealed class Configuration : DbMigrationsConfiguration<Blog.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Blog.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var demoPassword = WebConfigurationManager.AppSettings["DemoUserPassword"];

            if (!context.Users.Any(u => u.Email == "Bbeck7412@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Bbeck7412@gmail.com",
                    Email = "Bbeck7412@gmail.com",
                    FirstName = "Brandon",
                    LastName = "Beck",
                    DisplayName = "Brandon Beck"
                }, "Abc&123");
            }

            var adminId = userManager.FindByEmail("Bbeck7412@gmail.com").Id;
            userManager.AddToRole(adminId, "Admin");

        }
    }
}
