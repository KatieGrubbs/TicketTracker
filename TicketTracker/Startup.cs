using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using TicketTracker.Models;

[assembly: OwinStartupAttribute(typeof(TicketTracker.Startup))]
namespace TicketTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
        }

        private void CreateRoles()
        {
            using (var db = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                string[] roleNames = { "Admin" };
                IdentityResult roleResult;

                foreach (var roleName in roleNames)
                {
                    var roleExist = roleManager.RoleExists(roleName);

                    // if role does not exist
                    if (!roleExist)
                    {
                        //create the roles and seed them to the database: 
                        roleResult = roleManager.Create(new IdentityRole(roleName));
                    }
                }

                // find the user with the admin email 
                var user = userManager.FindByEmail("admin@gmail.com");

                // if the user does not exist
                if (user == null)
                {
                    // create superuser
                    var superuser = new ApplicationUser
                    {
                        UserName = "admin@gmail.com",
                        Email = "admin@gmail.com"
                    };

                    var adminPassword = "p@$$w0rd";

                    var createPowerUser = userManager.Create(superuser, adminPassword);

                    if (createPowerUser.Succeeded)
                    {
                        // make the superuser an Admin
                        userManager.AddToRole(superuser.Id, "Admin");
                    }
                }
            }
        }
    }
}
