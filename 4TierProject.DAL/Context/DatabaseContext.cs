using _4TierProject.Common.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4TierProject.DAL.Context
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>, IDbContext
    {
        public DatabaseContext()
        {
            //For Web Api
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(new DbInitializer());
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Cart> Carts { get; set; }
    }
}
