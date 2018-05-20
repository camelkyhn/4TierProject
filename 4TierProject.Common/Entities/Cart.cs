using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4TierProject.Common.Entities
{
    [Table("Carts")]
    public class Cart : EntityBase
    {
        public string CartID { get; set; }

        public int ItemCount { get; set; }

        public int? ProductID { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }
    }
}
