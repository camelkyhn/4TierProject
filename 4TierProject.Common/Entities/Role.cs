using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4TierProject.Common.Entities
{
    public class Role : IdentityRole
    {
        public Role() : base() { }

        public Role(string name) : base(name) { }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<ApplicationUser> AppUsers { get; set; }
    }
}
