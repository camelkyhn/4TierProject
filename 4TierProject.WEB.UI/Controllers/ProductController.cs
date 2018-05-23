using _4TierProject.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace _4TierProject.WEB.UI.Controllers
{
    [CustomAuthorize(Roles = "Admin,Manager")]
    public class ProductController : BaseController
    {
        //
        // GET: /ProductManager/
        public ActionResult Index()
        {
            var products = productRepo.GetList();
            return View(products.ToList());
        }

        //
        // GET: /StoreManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productRepo.FindByExpression(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // GET: /StoreManager/Create
        public ActionResult Create()
        {
            ViewBag.Categories = GetAllCategories;
            return View();
        }

        //
        // POST: /StoreManager/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Categories = GetAllCategories;

                    product.CreatedDate = DateTime.Now;
                    product.UpdatedDate = DateTime.Now;
                    product.CategoryName = catRepo.FindByExpression(p => p.Id == product.CategoryID).CategoryName;
                    productRepo.Insert(product);
                    ViewBag.IsSuccess = productRepo.Save() > 0 ? true : false;
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(product);
        }

        //
        // GET: /StoreManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productRepo.FindByExpression(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categories = GetAllCategories;
            return View(product);
        }

        //
        // POST: /StoreManager/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var createdDate = productRepo.FindByExpression(p => p.Id == product.Id).CreatedDate;
                product.CategoryName = catRepo.FindByExpression(p => p.Id == product.CategoryID).CategoryName;

                productRepo.Update(product);

                productRepo.FindByExpression(p => p.Id == product.Id).CreatedDate = createdDate;
                //productRepo.Find(product.ID).IsActive = true;  //Entry çağırıldıktan sonra ya da modified ifadesinden dolayı IsActive field'ı False'a düşüyor.
                productRepo.FindByExpression(p => p.Id == product.Id).UpdatedDate = DateTime.Now;
                productRepo.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = GetAllCategories;
            return View(product);
        }

        //
        // GET: /StoreManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productRepo.FindByExpression(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = productRepo.FindByExpression(p => p.Id == id);
            productRepo.Delete(product);
            productRepo.Save();
            return RedirectToAction("Index");
        }

        public IEnumerable<Category> GetAllCategories
        {
            get { return catRepo.GetList(); }
        }
    }
}