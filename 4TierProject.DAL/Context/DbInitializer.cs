using _4TierProject.Common.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4TierProject.DAL.Context
{
    public class DbInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DatabaseContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new DatabaseContext()));

            var user = new ApplicationUser()
            {
                UserName = "m.kemal.kayahan@hotmail.com",
                Email = "m.kemal.kayahan@hotmail.com",
                EmailConfirmed = true,
                FirstName = "Kemal",
                LastName = "Kayahan",
                IsActive = true,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Address = "Narl?dere",
                PhoneNumber = "+YourNumber",
                PhoneNumberConfirmed = true,
                LockoutEnabled = true
            };

            manager.Create(user, "password123");

            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new Role { Name = "Admin", IsActive = true });
                roleManager.Create(new Role { Name = "Manager", IsActive = true });
                roleManager.Create(new Role { Name = "User", IsActive = true });
            }

            var adminUser = manager.FindByName("m.kemal.kayahan@hotmail.com");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "Manager" });
        }
    }
}
