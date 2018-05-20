using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4TierProject.Common.Entities
{
    [Table("Products")]
    public class Product : EntityBase
    {
        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public short Stock { get; set; }

        public string ImageURL { get; set; }

        public decimal UnitPrice { get; set; }

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
