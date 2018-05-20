using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4TierProject.Common.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public Nullable<DateTime> CreatedDate { get; set; }

        public Nullable<DateTime> DeletedDate { get; set; }

        public Nullable<DateTime> UpdatedDate { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Adress { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string CompanyName { get; set; }

        public string ShippingName { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalPrice { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        public virtual ApplicationUser AppUser { get; set; }
    }
}
