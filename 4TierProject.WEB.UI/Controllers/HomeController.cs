using _4TierProject.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4TierProject.WEB.UI.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        public ActionResult Index(string UserName)
        {
            // Get most popular albums
            var products = GetTopSellingProducts(5);

            if (User.Identity.Name == null)
            {
                return View(products);
            }
            else
            {
                ViewBag.UserName = User.Identity.Name;

                return View(products);
            }
        }

        private List<Product> GetTopSellingProducts(int count)
        {
            // Group the order details by product and return
            // the products with the highest count
            return productRepo.GetList().Take(count).ToList();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}