using _4TierProject.Common.Entities;
using _4TierProject.WEB.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace _4TierProject.WEB.UI.Controllers
{
    [CustomAuthorize(Roles = "Admin,Manager")]
    public class RoleController : BaseController
    {
        // GET: Role
        public ActionResult Index()
        {
            List<RoleViewModel> list = new List<RoleViewModel>();
            foreach (var item in RoleManager.Roles)
            {
                list.Add(new RoleViewModel(item));
            }
            return View(list);
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel model)
        {
            var role = new Role() { Name = model.Name };
            await RoleManager.CreateAsync(role);

            return RedirectToAction("Index");
        }

        //GET
        public async Task<ActionResult> Edit(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }

        //POST
        [HttpPost]
        public async Task<ActionResult> Edit(RoleViewModel model)
        {
            var role = new Role() { Id = model.Id, Name = model.Name };
            await RoleManager.UpdateAsync(role);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }

        //GET
        public async Task<ActionResult> Delete(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }

        //POST
        [HttpPost]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            await RoleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }
    }
}