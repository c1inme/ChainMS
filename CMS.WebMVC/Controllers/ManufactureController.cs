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
    public class ManufactureController : Controller
    {
        private DBServerContext db = new DBServerContext();

        //
        // GET: /Manufacture/

        public ActionResult Index()
        {
            return View(db.Manufactures.ToList());
        }

        //
        // GET: /Manufacture/Details/5

        public ActionResult Details(Guid? id = null)
        {
            Manufacture manufacture = db.Manufactures.Find(id);
            if (manufacture == null)
            {
                return HttpNotFound();
            }
            return View(manufacture);
        }

        //
        // GET: /Manufacture/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Manufacture/Create

        [HttpPost]
        public ActionResult Create(Manufacture manufacture)
        {
            if (ModelState.IsValid)
            {
                manufacture.GuidId = Guid.NewGuid();
                db.Manufactures.Add(manufacture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manufacture);
        }

        //
        // GET: /Manufacture/Edit/5

        public ActionResult Edit(Guid? id = null)
        {
            Manufacture manufacture = db.Manufactures.Find(id);
            if (manufacture == null)
            {
                return HttpNotFound();
            }
            return View(manufacture);
        }

        //
        // POST: /Manufacture/Edit/5

        [HttpPost]
        public ActionResult Edit(Manufacture manufacture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manufacture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manufacture);
        }

        //
        // GET: /Manufacture/Delete/5

        public ActionResult Delete(Guid? id = null)
        {
            Manufacture manufacture = db.Manufactures.Find(id);
            if (manufacture == null)
            {
                return HttpNotFound();
            }
            return View(manufacture);
        }

        //
        // POST: /Manufacture/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Manufacture manufacture = db.Manufactures.Find(id);
            db.Manufactures.Remove(manufacture);
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