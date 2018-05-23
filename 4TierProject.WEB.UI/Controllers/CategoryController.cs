using _4TierProject.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace _4TierProject.WEB.UI.Controllers
{
    [CustomAuthorize(Roles = "Admin,Manager")]
    public class CategoryController : BaseController
    {
        // GET: CategoryManager
        public ActionResult Index()
        {
            var categories = catRepo.GetList();
            return View(categories.ToList());
        }

        // GET: CategoryManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = catRepo.FindByExpression(c => c.Id == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: CategoryManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryManager/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CategoryName,Description,ImagePath,IsActive,IsDeleted")] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    catRepo.Insert(category);
                    ViewBag.IsSuccess = catRepo.Save() > 0 ? true : false;
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {

                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(category);
        }

        // GET: CategoryManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = catRepo.FindByExpression(c => c.Id == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: CategoryManager/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CategoryName,Description,ImagePath,IsActive,IsDeleted")] Category category)
        {
            if (ModelState.IsValid)
            {
                catRepo.Update(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: CategoryManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = catRepo.FindByExpression(c => c.Id == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: CategoryManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = catRepo.FindByExpression(c => c.Id == id);
            catRepo.Delete(category);
            catRepo.Save();
            return RedirectToAction("Index");
        }
    }
}