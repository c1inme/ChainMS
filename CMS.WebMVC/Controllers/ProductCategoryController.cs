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
    public class ProductCategoryController : Controller
    {
        private DBServerContext db = new DBServerContext();

        //
        // GET: /ProductCategory/

        public ActionResult Index()
        {
            var productcategorys = db.ProductCategorys.Include(p => p.ParentProductCategory);
            return View(productcategorys.ToList());
        }

        //
        // GET: /ProductCategory/Details/5

        public ActionResult Details(Guid? id = null)
        {
            ProductCategory productcategory = db.ProductCategorys.Find(id);
            if (productcategory == null)
            {
                return HttpNotFound();
            }
            return View(productcategory);
        }

        //
        // GET: /ProductCategory/Create

        public ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(db.ProductCategorys, "GuidId", "Name");
            return View();
        }

        //
        // POST: /ProductCategory/Create

        [HttpPost]
        public ActionResult Create(ProductCategory productcategory)
        {
            if (ModelState.IsValid)
            {
                productcategory.GuidId = Guid.NewGuid();
                db.ProductCategorys.Add(productcategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParentId = new SelectList(db.ProductCategorys, "GuidId", "Name", productcategory.ParentId);
            return View(productcategory);
        }

        //
        // GET: /ProductCategory/Edit/5

        public ActionResult Edit(Guid? id = null)
        {
            ProductCategory productcategory = db.ProductCategorys.Find(id);
            if (productcategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentId = new SelectList(db.ProductCategorys, "GuidId", "Name", productcategory.ParentId);
            return View(productcategory);
        }

        //
        // POST: /ProductCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(ProductCategory productcategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productcategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.ProductCategorys, "GuidId", "Name", productcategory.ParentId);
            return View(productcategory);
        }

        //
        // GET: /ProductCategory/Delete/5

        public ActionResult Delete(Guid? id = null)
        {
            ProductCategory productcategory = db.ProductCategorys.Find(id);
            if (productcategory == null)
            {
                return HttpNotFound();
            }
            return View(productcategory);
        }

        //
        // POST: /ProductCategory/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ProductCategory productcategory = db.ProductCategorys.Find(id);
            db.ProductCategorys.Remove(productcategory);
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