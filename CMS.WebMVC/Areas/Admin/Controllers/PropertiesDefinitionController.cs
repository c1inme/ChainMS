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
    public class PropertiesDefinitionController : Controller
    {
        private DBServerContext db = new DBServerContext();

        //
        // GET: /Admin/PropertiesDefinition/

        public ActionResult Index()
        {
            return View(db.PropertiesDefinitions.ToList());
        }

        //
        // GET: /Admin/PropertiesDefinition/Details/5

        public ActionResult Details(Guid id)
        {
            PropertiesDefinition propertiesdefinition = db.PropertiesDefinitions.Find(id);
            if (propertiesdefinition == null)
            {
                return HttpNotFound();
            }
            return View(propertiesdefinition);
        }

        //
        // GET: /Admin/PropertiesDefinition/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/PropertiesDefinition/Create

        [HttpPost]
        public ActionResult Create(PropertiesDefinition propertiesdefinition)
        {
            if (ModelState.IsValid)
            {
                propertiesdefinition.GuidId = Guid.NewGuid();
                db.PropertiesDefinitions.Add(propertiesdefinition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(propertiesdefinition);
        }

        //
        // GET: /Admin/PropertiesDefinition/Edit/5

        public ActionResult Edit(Guid id)
        {
            PropertiesDefinition propertiesdefinition = db.PropertiesDefinitions.Find(id);
            if (propertiesdefinition == null)
            {
                return HttpNotFound();
            }
            return View(propertiesdefinition);
        }

        //
        // POST: /Admin/PropertiesDefinition/Edit/5

        [HttpPost]
        public ActionResult Edit(PropertiesDefinition propertiesdefinition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propertiesdefinition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(propertiesdefinition);
        }

        //
        // GET: /Admin/PropertiesDefinition/Delete/5

        public ActionResult Delete(Guid id)
        {
            PropertiesDefinition propertiesdefinition = db.PropertiesDefinitions.Find(id);
            if (propertiesdefinition == null)
            {
                return HttpNotFound();
            }
            return View(propertiesdefinition);
        }

        //
        // POST: /Admin/PropertiesDefinition/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            PropertiesDefinition propertiesdefinition = db.PropertiesDefinitions.Find(id);
            db.PropertiesDefinitions.Remove(propertiesdefinition);
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