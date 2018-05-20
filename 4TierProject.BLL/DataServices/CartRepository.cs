using _4TierProject.Common.Entities;
using _4TierProject.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace _4TierProject.BLL.DataServices
{
    public partial class CartRepository
    {
        protected DatabaseContext _context;

        public CartRepository()
        {
            _context = new DatabaseContext();
        }

        string ShoppingCartID { get; set; }

        public const string CartSessionKey = "CartId";

        public static CartRepository GetCart(HttpContextBase context)
        {
            var cart = new CartRepository();
            cart.ShoppingCartID = cart.GetCartId(context);
            return cart;
        }

        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        //Unmanagement Resource
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public void AddToCart(Product product)
        {
            // Get the matching cart and product instances
            var cartItem = _context.Set<Cart>().SingleOrDefault(
                c => c.CartID == ShoppingCartID
                && c.ProductID == product.Id);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    ProductID = product.Id,
                    ProductName = product.ProductName,
                    ProductPrice = product.UnitPrice,
                    CartID = ShoppingCartID,
                    ItemCount = 1,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };
                _context.Set<Cart>().Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.ItemCount++;
            }
            // Save changes
            _context.SaveChanges();
        }

        public void RemoveFromCart(Product product)
        {
            // Get the cart
            var cartItem = _context.Set<Cart>().SingleOrDefault(
                c => c.CartID == ShoppingCartID
                && c.ProductID == product.Id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.ItemCount > 1)
                {
                    cartItem.ItemCount--;
                    itemCount = cartItem.ItemCount;
                }
                else
                {
                    _context.Set<Cart>().Remove(cartItem);
                }
                // Save changes
                _context.SaveChanges();
            }
        }

        public void EmptyCart()
        {
            var cartItems = _context.Set<Cart>().Where(
                cart => cart.CartID == ShoppingCartID);

            foreach (var cartItem in cartItems)
            {
                _context.Set<Cart>().Remove(cartItem);
            }
            // Save changes
            _context.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return _context.Set<Cart>().Where(
                cart => cart.CartID == ShoppingCartID).ToList();
        }

        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in _context.Set<Cart>()
                          where cartItems.CartID == ShoppingCartID
                          select (int?)cartItems.ItemCount).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            // Multiply album price by count of that product to get 
            // the current price for each of those products in the cart
            // sum all product price totals to get the cart total
            decimal? total = (from cartItems in _context.Set<Cart>()
                              where cartItems.CartID == ShoppingCartID
                              select (int?)cartItems.ItemCount *
                              cartItems.ProductPrice).Sum();

            return total ?? decimal.Zero;
        }

        public List<Product> GetProductsFromCart()
        {
            List<Product> productList = new List<Product>();
            List<Cart> cartItems = GetCartItems();
            foreach (var item in cartItems)
            {
                Product product = _context.Set<Product>().FirstOrDefault(p => p.Id == item.ProductID);
                productList.Add(product);
            }
            return productList;
        }

        public long CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductID = item.ProductID,
                    OrderID = order.Id,
                    UnitPrice = item.ProductPrice,
                    Quantity = item.ItemCount,
                    IsActive = true
                };
                // Set the order total of the shopping cart
                orderTotal += (item.ItemCount * item.ProductPrice);

                _context.Set<Product>().Find(item.ProductID).Stock--;
                _context.Set<OrderDetail>().Add(orderDetail);

            }

            _context.Entry(order).State = EntityState.Modified;

            // Set the order's total to the orderTotal count
            _context.Set<Order>().Find(order.Id).TotalPrice = orderTotal;
            _context.Set<Order>().Find(order.Id).CreatedDate = DateTime.Now;
            _context.Set<Order>().Find(order.Id).UpdatedDate = DateTime.Now;
            _context.Set<Order>().Find(order.Id).IsActive = true;

            // Save the order
            _context.SaveChanges();
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return order.Id;
        }

        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = _context.Set<Cart>().Where(
                c => c.CartID == ShoppingCartID);

            foreach (Cart item in shoppingCart)
            {
                item.CartID = userName;
            }
            _context.SaveChanges();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
