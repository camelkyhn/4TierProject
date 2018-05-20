using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4TierProject.Common.Entities
{
    [Table("Categories")]
    public class Category : EntityBase
    {
        public string CategoryName { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
