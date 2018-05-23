using _4TierProject.BLL.DataServices;
using _4TierProject.Common.Entities;
using _4TierProject.WEB.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4TierProject.WEB.UI.Controllers
{
    public class CartController : BaseController
    {
        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var cart = CartRepository.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new CartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Return the view
            return View(viewModel);
        }
        //
        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(int id)
        {
            // Retrieve the album from the database
            var addedProduct = productRepo.FindByExpression(p => p.Id == id);

            // Add it to the shopping cart
            var cart = CartRepository.GetCart(this.HttpContext);

            cart.AddToCart(addedProduct);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        //
        // GET: /StoreManager/Delete/5
        public ActionResult RemoveFromCart(int id)
        {
            return View(productRepo.FindByExpression(p => p.Id == id));
        }

        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(Product product)
        {
            // Remove the item from the cart
            var cart = CartRepository.GetCart(this.HttpContext);

            // Get the name of the product to display confirmation
            //string productName = cart.FirstOrDefault(item => item.ID == id).Product.ProductName;

            cart.RemoveFromCart(product);

            return RedirectToAction("Index");
        }
        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = CartRepository.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}