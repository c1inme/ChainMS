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
    public class ProductController : Controller
    {
        private DBServerContext db = new DBServerContext();

        //
        // GET: /Product/

        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Manufacture).Include(p => p.ProductCategory);
            return View(products.ToList());
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(Guid? id = null)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            ViewBag.ManufactureId = new SelectList(db.Manufactures, "GuidId", "Name");
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategorys, "GuidId", "Name");
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.GuidId = Guid.NewGuid();
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ManufactureId = new SelectList(db.Manufactures, "GuidId", "Name", product.ManufactureId);
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategorys, "GuidId", "Name", product.ProductCategoryId);
            return View(product);
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(Guid? id = null)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManufactureId = new SelectList(db.Manufactures, "GuidId", "Name", product.ManufactureId);
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategorys, "GuidId", "Name", product.ProductCategoryId);
            return View(product);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManufactureId = new SelectList(db.Manufactures, "GuidId", "Name", product.ManufactureId);
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategorys, "GuidId", "Name", product.ProductCategoryId);
            return View(product);
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(Guid? id = null)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Product/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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