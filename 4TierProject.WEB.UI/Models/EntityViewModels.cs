using _4TierProject.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4TierProject.WEB.UI.Models
{
    public class CartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }

    public class CartRemoveViewModel
    {
        public string Message { get; set; }
        public decimal CartTotal { get; set; }
        public int CartCount { get; set; }
        public int ItemCount { get; set; }
        public int DeleteId { get; set; }
    }
}