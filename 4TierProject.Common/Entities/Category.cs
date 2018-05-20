using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4TierProject.Common.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
