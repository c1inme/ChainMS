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
    public class ManufactureController : Controller
    {
        private DBServerContext db = new DBServerContext();
        CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
       
        //
        // GET: /Admin/Manufacture/

        public ActionResult Index()
        {
            var result = service.GetManufacture(null);
            return View(result);
        }

        //
        // GET: /Admin/Manufacture/Details/5

        public ActionResult Details(Guid id )
        {
            var result = service.GetManufactureByKey(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        //
        // GET: /Admin/Manufacture/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Manufacture/Create

        [HttpPost]
        public ActionResult Create(Manufacture manufacture)
        {
            if (ModelState.IsValid)
            {
                if (manufacture.Picture != null)
                {
                    string path = "/ImageRepoisitory/manufacture/" + manufacture.GuidId;
                    string fileNameExpected = "_" + manufacture.Name + "_Manufacture";
                    manufacture.IconImage = StaticHelper.SaveFileImage(path, manufacture.Picture, fileNameExpected);
                }
                CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
                service.SaveManufacture(manufacture);
                return RedirectToAction("Index");
            }

            return View(manufacture);
        }

        //
        // GET: /Admin/Manufacture/Edit/5

        public ActionResult Edit(Guid id )
        {
            Manufacture manufacture = db.Manufactures.Find(id);
            if (manufacture == null)
            {
                return HttpNotFound();
            }
            return View(manufacture);
        }

        //
        // POST: /Admin/Manufacture/Edit/5

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
        // GET: /Admin/Manufacture/Delete/5

        public ActionResult Delete(Guid id )
        {
            Manufacture manufacture = db.Manufactures.Find(id);
            if (manufacture == null)
            {
                return HttpNotFound();
            }
            return View(manufacture);
        }

        //
        // POST: /Admin/Manufacture/Delete/5

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