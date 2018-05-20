using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4TierProject.Common.Entities
{
    public class Cart
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public Nullable<DateTime> CreatedDate { get; set; }

        public Nullable<DateTime> DeletedDate { get; set; }

        public Nullable<DateTime> UpdatedDate { get; set; }

        public string CartID { get; set; }

        public int ItemCount { get; set; }

        public int? ProductID { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }
    }
}
