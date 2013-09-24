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
    public class MenuCategoryController : Controller
    {
        private DBServerContext db = new DBServerContext();

        //
        // GET: /Admin/MenuCategory/

        public ActionResult Index(string codeLang)
        {
            CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();

            var langs = service.GetLanguage(null);
            ViewBag.Languages = langs;
            Language firstLang;
            if (string.IsNullOrEmpty(codeLang))
            {
                firstLang = langs.FirstOrDefault();
            }
            else
            {
                firstLang = langs.FirstOrDefault(f => f.CodeLanguage == codeLang);
            }
            var menucategorys = service.GetMenuCategory(f => f.LanguageId == firstLang.GuidId);
            return View(menucategorys.ToList());
        }



        //
        // GET: /Admin/MenuCategory/Details/5

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
        // GET: /Admin/MenuCategory/Create

        public ActionResult Create(string guidParent, string codeLang)
        {
            if (guidParent == null)
                guidParent = Guid.Empty.ToString();
            CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
            List<MenuCategory> lstParent = service.GetMenuCategory(f => f.Lang.CodeLanguage.Equals(string.IsNullOrEmpty(codeLang) ? "vi" : codeLang));
            lstParent.Insert(0, new MenuCategory() { GuidId = Guid.Empty, Name = "None" });
            ViewBag.ListMenuCategory = lstParent;
            MenuCategory forCreate = new MenuCategory();
            forCreate.ParentId = new Guid(guidParent);
            return View(forCreate);
        }

        //
        // POST: /Admin/MenuCategory/Create

        [HttpPost]
        public ActionResult Create(MenuCategory menucategory)
        {
            if (ModelState.IsValid)
            {
                if (menucategory.Picture != null)
                {
                    string path = "/ImageRepoisitory/MenuCategory/" + menucategory.GuidId;
                    string fileNameExpected = "_" + menucategory.Name + "_MenuCategory";
                    menucategory.IconImage = StaticHelper.SaveFileImage(path, menucategory.Picture, fileNameExpected);
                }
                CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
                service.SaveMenuCategory(menucategory, false);
                return RedirectToAction("Index");
            }

            //ViewBag.ParentId = new SelectList(db.MenuCategorys, "GuidId", "Name", menucategory.ParentId);
            return View(menucategory);
        }

        //
        // GET: /Admin/MenuCategory/Edit/5

        public ActionResult Edit(Guid id)
        {
            CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
            MenuCategory menucategory = service.GetMenuCategoryByKey(id);
            if (menucategory == null)
            {
                return HttpNotFound();
            }
            List<MenuCategory> lstParent = service.GetMenuCategory(null);
            lstParent.Insert(0, new MenuCategory() { GuidId = Guid.Empty, Name = "None" });
            ViewBag.ListMenuCategory = lstParent;
            return View(menucategory);
        }

        //
        // POST: /Admin/MenuCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(MenuCategory menucategory)
        {
            if (ModelState.IsValid)
            {
                if (menucategory.Picture != null)
                {
                    string path = "/ImageRepoisitory/MenuCategory/" + menucategory.GuidId;
                    string fileNameExpected = "_" + menucategory.Name + "_MenuCategory";
                    menucategory.IconImage = StaticHelper.SaveFileImage(path, menucategory.Picture, fileNameExpected);
                }
                CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
                menucategory = service.SaveMenuCategory(menucategory, false);
                return RedirectToAction("Index");
            }
            // ViewBag.ParentId = new SelectList(db.MenuCategorys, "GuidId", "Name", menucategory.ParentId);
            return View(menucategory);
        }

        //
        // GET: /Admin/MenuCategory/Delete/5

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
        // POST: /Admin/MenuCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
            service.DeleteMenuCategoryByKey(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}