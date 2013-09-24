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
    public class NewsController : Controller
    {
        private DBServerContext db = new DBServerContext();

        //
        // GET: /News/

        public ActionResult Index()
        {
            var newss = db.Newss.Include(n => n.MenuCategory);
            return View(newss.ToList());
        }

        //
        // GET: /News/Details/5

        public ActionResult Details(Guid? id = null)
        {
            News news = db.Newss.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //
        // GET: /News/Create

        public ActionResult Create()
        {
            ViewBag.MenuId = new SelectList(db.MenuCategorys, "GuidId", "Name");
            return View();
        }

        //
        // POST: /News/Create

        [HttpPost]
        public ActionResult Create(News news)
        {
            if (ModelState.IsValid)
            {
                news.GuidId = Guid.NewGuid();
                db.Newss.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MenuId = new SelectList(db.MenuCategorys, "GuidId", "Name", news.MenuId);
            return View(news);
        }

        //
        // GET: /News/Edit/5

        public ActionResult Edit(Guid? id = null)
        {
            News news = db.Newss.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.MenuId = new SelectList(db.MenuCategorys, "GuidId", "Name", news.MenuId);
            return View(news);
        }

        //
        // POST: /News/Edit/5

        [HttpPost]
        public ActionResult Edit(News news)
        {
            if (ModelState.IsValid)
            {
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MenuId = new SelectList(db.MenuCategorys, "GuidId", "Name", news.MenuId);
            return View(news);
        }

        //
        // GET: /News/Delete/5

        public ActionResult Delete(Guid? id = null)
        {
            News news = db.Newss.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //
        // POST: /News/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            News news = db.Newss.Find(id);
            db.Newss.Remove(news);
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