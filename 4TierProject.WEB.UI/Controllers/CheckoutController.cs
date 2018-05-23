using _4TierProject.BLL.DataServices;
using _4TierProject.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4TierProject.WEB.UI.Controllers
{
    public class CheckoutController : BaseController
    {
        const string PromoCode = "FREE";

        ////
        //// GET: /Checkout/AddressAndPayment
        public ActionResult AddressAndPayment()
        {
            return View();
        }

        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        [CustomAuthorize(Roles = "Admin,Manager,User")]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return View(order);
                }
                else
                {
                    //Process the order
                    var cart = CartRepository.GetCart(this.HttpContext);

                    if (cart.GetCount() == 0)
                    {
                        ViewBag.Message = "Cart Empty!";
                        return View();
                    }
                    else
                    {
                        order.UserName = User.Identity.Name;
                        order.OrderDate = DateTime.Now;
                        //Save Order
                        orderRepo.Insert(order);
                        orderRepo.Save();

                        cart.CreateOrder(order);

                        return RedirectToAction("Complete", "Checkout",
                            new { id = order.Id });
                    }
                }
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }

        //
        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = orderRepo.Any(
                o => o.Id == id &&
                o.UserName == User.Identity.Name);

            if (isValid)
            {
                var cart = CartRepository.GetCart(this.HttpContext);
                cart.EmptyCart();
                cart.Save();
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}