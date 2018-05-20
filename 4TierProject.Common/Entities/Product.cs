using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4TierProject.Common.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public Nullable<DateTime> CreatedDate { get; set; }

        public Nullable<DateTime> DeletedDate { get; set; }

        public Nullable<DateTime> UpdatedDate { get; set; }

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
