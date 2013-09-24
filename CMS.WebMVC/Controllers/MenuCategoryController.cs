using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Entities.ServerObjects;

namespace CMS.WebMVC.Controllers
{
    public class MenuCategoryController : Controller
    {
        private DBServerContext db = new DBServerContext();

        //
        // GET: /MenuCategory/

        public ActionResult Index()
        {
            var menucategorys = db.MenuCategorys.Include(m => m.ParentMenuCategory);
            return View(menucategorys.ToList());
        }

        //
        // GET: /MenuCategory/Details/5

        public ActionResult Details(Guid? id = null)
        {
            MenuCategory menucategory = db.MenuCategorys.Find(id);
            if (menucategory == null)
            {
                return HttpNotFound();
            }
            return View(menucategory);
        }

        //
        // GET: /MenuCategory/Create

        public ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(db.MenuCategorys, "GuidId", "Name");
            return View();
        }

        //
        // POST: /MenuCategory/Create

        [HttpPost]
        public ActionResult Create(MenuCategory menucategory)
        {
            if (ModelState.IsValid)
            {
                menucategory.GuidId = Guid.NewGuid();
                db.MenuCategorys.Add(menucategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParentId = new SelectList(db.MenuCategorys, "GuidId", "Name", menucategory.ParentId);
            return View(menucategory);
        }

        //
        // GET: /MenuCategory/Edit/5

        public ActionResult Edit(Guid? id = null)
        {
            MenuCategory menucategory = db.MenuCategorys.Find(id);
            if (menucategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentId = new SelectList(db.MenuCategorys, "GuidId", "Name", menucategory.ParentId);
            return View(menucategory);
        }

        //
        // POST: /MenuCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(MenuCategory menucategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menucategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.MenuCategorys, "GuidId", "Name", menucategory.ParentId);
            return View(menucategory);
        }

        //
        // GET: /MenuCategory/Delete/5

        public ActionResult Delete(Guid? id = null)
        {
            MenuCategory menucategory = db.MenuCategorys.Find(id);
            if (menucategory == null)
            {
                return HttpNotFound();
            }
            return View(menucategory);
        }

        //
        // POST: /MenuCategory/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            MenuCategory menucategory = db.MenuCategorys.Find(id);
            db.MenuCategorys.Remove(menucategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}