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
    public class LanguageController : Controller
    {
        private DBServerContext db = new DBServerContext();
        CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
        //
        // GET: /Admin/Language/
        public ActionResult Index()
        {
            return View(db.Languages.ToList());
        }

        //
        // GET: /Admin/Language/Details/5

        public ActionResult Details(Guid id)
        {
            Language language = db.Languages.Find(id);
            if (language == null)
            {
                return HttpNotFound();
            }
            return View(language);
        }

        //
        // GET: /Admin/Language/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Language/Create

        [HttpPost]
        public ActionResult Create(Language language)
        {
            if (ModelState.IsValid)
            {
                if (language.Picture != null)
                {
                    string path = "/ImageRepoisitory/language/" + language.CodeLanguage + "/" + language.GuidId;
                    string fileNameExpected = "_" + language.CodeLanguage + "_Flag";
                    string extension = System.IO.Path.GetExtension(language.Picture.FileName);
                    var imgThumb = Images.CreateThumbnailToImage(language.Picture.InputStream, TypePathImage.Thumbnail);
                    language.PathFlagLanguage = StaticHelper.SaveImage(path, TypePathImage.Large, imgThumb, fileNameExpected, extension);
                }
                service.SaveLanguage(language);
                return RedirectToAction("Index");
            }

            return View(language);
        }

        //
        // GET: /Admin/Language/Edit/5

        public ActionResult Edit(Guid id)
        {
            Language language = db.Languages.Find(id);
            if (language == null)
            {
                return HttpNotFound();
            }
            return View(language);
        }

        //
        // POST: /Admin/Language/Edit/5

        [HttpPost]
        public ActionResult Edit(Language language)
        {
            if (ModelState.IsValid)
            {
                if (language.Picture != null)
                {
                    string path = "/ImageRepoisitory/language/" + language.CodeLanguage + "/" + language.GuidId;
                    string fileNameExpected = "_" + language.CodeLanguage + "_Flag";
                    string extension = System.IO.Path.GetExtension(language.Picture.FileName);
                    var imgThumb = Images.CreateThumbnailToImage(language.Picture.InputStream, TypePathImage.Thumbnail);
                    language.PathFlagLanguage = StaticHelper.SaveImage(path, TypePathImage.Large, imgThumb, fileNameExpected, extension);
                }
                service.SaveLanguage(language);
                return RedirectToAction("Index");
            }
            return View(language);
        }

        //
        // GET: /Admin/Language/Delete/5

        public ActionResult Delete(Guid id)
        {
            Language language = db.Languages.Find(id);
            if (language == null)
            {
                return HttpNotFound();
            }
            return View(language);
        }

        //
        // POST: /Admin/Language/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Language language = db.Languages.Find(id);
            db.Languages.Remove(language);
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