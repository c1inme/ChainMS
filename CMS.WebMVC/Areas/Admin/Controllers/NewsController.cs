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
    public class NewsController : Controller
    {
        private DBServerContext db = new DBServerContext();

        //
        // GET: /Admin/News/

        public ActionResult Index()
        {
            var newss = db.Newss.Include(n => n.MenuCategory).Include(n => n.MenuCategory.ParentMenuCategory);
            var menus = StaticHelper.BuildNewsGroupByMenu(newss.ToList());
            return View(menus);
        }

        //
        // GET: /Admin/News/Details/5
        public ActionResult EditNewsForProduct(Guid id)
        {
            CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
            var news = service.GetNewByKey(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //
        // GET: /Admin/News/Details/5
        public ActionResult Details(Guid id)
        {
            CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
            var news = service.GetNewByKey(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }
        //
        // GET: /Admin/News/Create
        public ActionResult CreateNewsForProduct(string  discriminator, Guid IdBelong)
        {
            var news = new News();
            news.Title = discriminator + "--" + IdBelong.ToString();
            news.Discriminator = discriminator;
            news.IdBelong = IdBelong;
            return View(news);
        }

        //
        // GET: /Admin/News/Create
        public ActionResult Create()
        {
            CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
            List<MenuCategory> lstParent = service.GetMenuCategory(null);
            ViewBag.ListMenuCategory = lstParent;
            return View();
        }

        //
        // POST: /Admin/News/Create

        [HttpPost]
        public ActionResult Create(News news)
        {
            if (ModelState.IsValid)
            {
                if (news.Picture != null)
                {
                    string path = "/ImageRepoisitory/news/" + news.GuidId;
                    string fileNameExpected = "_" + news.Title + "_news";
                    news.ImagePath = StaticHelper.SaveFileImage(path, news.Picture, fileNameExpected);
                }

                CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
                service.SaveNews(news);
                
                //news.GuidId = Guid.NewGuid();
                //db.Newss.Add(news);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MenuId = new SelectList(db.MenuCategorys, "GuidId", "Name", news.MenuId);
            return View(news);
        }

        //
        // GET: /Admin/News/Edit/5

        public ActionResult Edit(Guid id)
        {
            CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
            var news = service.GetNewByKey(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            List<MenuCategory> lstParent = service.GetMenuCategory(null);
            ViewBag.ListMenuCategory = lstParent;
            return View(news);
        }

        //
        // POST: /Admin/News/Edit/5

        [HttpPost]
        public ActionResult Edit(News news)
        {
            if (ModelState.IsValid)
            {
                if (news.Picture != null)
                {
                    string path = "/ImageRepoisitory/news/" + news.GuidId;
                    string fileNameExpected = "_" + news.Title + "_news";
                    news.ImagePath = StaticHelper.SaveFileImage(path, news.Picture, fileNameExpected);
                }
                CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
                service.SaveNews(news);
                return RedirectToAction("Index");
            }
            ViewBag.MenuId = new SelectList(db.MenuCategorys, "GuidId", "Name", news.MenuId);
            return View(news);
        }

        //
        // GET: /Admin/News/Delete/5

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
        // POST: /Admin/News/Delete/5

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