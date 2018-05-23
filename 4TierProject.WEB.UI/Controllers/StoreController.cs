using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace _4TierProject.WEB.UI.Controllers
{
    [AllowAnonymous]
    public class StoreController : BaseController
    {
        // GET: Product
        public ActionResult Index()
        {
            var model = catRepo.GetList();
            return View(model);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = productRepo.FindByExpression(x => x.Id == id);
            ViewBag.productCategoryName = catRepo.FindByExpression(c => c.Id == product.CategoryID).CategoryName;
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // GET: /Store/Browse?category=giyim
        public ActionResult Browse(string category)
        {
            // Retrieve Category and its Associated Products from database
            var categoryModel = catRepo.Include("Products").Single(g => g.CategoryName == category);

            return View(categoryModel);
        }

        //
        // GET: /Product/CategoryMenu
        [ChildActionOnly]
        public ActionResult CategoryMenu()
        {
            var categories = catRepo.GetList();
            return PartialView(categories);
        }
    }
}