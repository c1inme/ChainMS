using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Entities.ServerObjects;

namespace CMS.WebMVC.Areas.Admin.Controllers
{
    public class GalleryController : Controller
    {
        private DBServerContext db = new DBServerContext();

        //
        // GET: /Admin/Gallery/

        public ActionResult Index()
        {
            return View(db.Gallerys.ToList());
        }

        //
        // GET: /Admin/Gallery/Details/5

        public ActionResult Details(Guid id )
        {
            Gallery gallery = db.Gallerys.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        //
        // GET: /Admin/Gallery/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Gallery/Create

        [HttpPost]
        public ActionResult Create(Gallery gallery)
        {
            if (ModelState.IsValid)
            {
                gallery.GuidId = Guid.NewGuid();
                db.Gallerys.Add(gallery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gallery);
        }

        //
        // GET: /Admin/Gallery/Edit/5

        public ActionResult Edit(Guid id )
        {
            Gallery gallery = db.Gallerys.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        //
        // POST: /Admin/Gallery/Edit/5

        [HttpPost]
        public ActionResult Edit(Gallery gallery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gallery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gallery);
        }

        //
        // GET: /Admin/Gallery/Delete/5

        public ActionResult Delete(Guid id )
        {
            Gallery gallery = db.Gallerys.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        //
        // POST: /Admin/Gallery/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Gallery gallery = db.Gallerys.Find(id);
            db.Gallerys.Remove(gallery);
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