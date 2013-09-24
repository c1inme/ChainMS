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
    public class ImageAdminController : Controller
    {
        private DBServerContext db = new DBServerContext();
        CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
        //
        // GET: /Admin/ImageAdmin/

        public ActionResult Index()
        {
            return View(db.Images.ToList());
        }

        //
        // GET: /Admin/ImageAdmin/Details/5
        public ActionResult Details(Guid id)
        {
            Image image = service.GetImageByKey(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }
         //
        // GET: /Admin/ImageAdmin/ImageByDiscriminator/Discriminator/IdBelong
        public ActionResult ImageByDiscriminator(string Discriminator, Guid IdBelong)
        {
            var result = service.GetImage(f => f.IdBelong == IdBelong && f.Discriminator == Discriminator);
            ViewBag.discriminator = Discriminator;
            ViewBag.IdBelong = IdBelong;
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        //
        // GET: /Admin/ImageAdmin/Create/Product/5
        public ActionResult Create(string Discriminator, Guid IdBelong)
        {
            var img = new Image();
            img.Discriminator = Discriminator;
            img.IdBelong = IdBelong;
            return View(img);
        }

        ////
        //// GET: /Admin/ImageAdmin/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //
        // POST: /Admin/ImageAdmin/Create

        [HttpPost]
        public ActionResult Create(Image image)
        {
            if (ModelState.IsValid)
            {
                if (image.Picture != null)
                {
                    string path = "/ImageRepoisitory/images/" + image.Discriminator + "/" + image.GuidId;
                    string fileNameExpected = "_" + image.Name + "_ImageDetail";
                    string extension = System.IO.Path.GetExtension(image.Picture.FileName);

                    var imgLarge = Images.CreateThumbnailToImage(image.Picture.InputStream, TypePathImage.Large);
                    var imgSmall = Images.CreateThumbnailToImage(image.Picture.InputStream, TypePathImage.Small);
                    var imgThumb = Images.CreateThumbnailToImage(image.Picture.InputStream, TypePathImage.Thumbnail);

                    image.FullHdPath = StaticHelper.SaveImage(path, TypePathImage.Large, imgLarge, fileNameExpected, extension);
                    image.SmallPath = StaticHelper.SaveImage(path, TypePathImage.Small, imgSmall, fileNameExpected, extension);
                    image.ThumpnailPath = StaticHelper.SaveImage(path, TypePathImage.Thumbnail, imgThumb, fileNameExpected, extension);
                }
                CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
                service.SaveImage(image);
                return RedirectToAction("ImageByDiscriminator", new { Discriminator = image.Discriminator, IdBelong = image.IdBelong});
            }

            return View(image);
        }

        public static void CreateImageRelation(HttpPostedFileBase file, Guid IdBlong, string discriminator, CMS.WCFService.ServicesServer.ServerService service = null)
        {
            var img = new Image();
            if (file != null)
            {

                img.Name = "Primary_" + discriminator;
                img.Discriminator = discriminator;
                img.GuidId = IdBlong;
                img.IdBelong = IdBlong;
                string path = "/ImageRepoisitory/images/" + discriminator + "/" + img.GuidId;
                string fileNameExpected = "_" + img.Name + "_ImageDetail";
                string extension = System.IO.Path.GetExtension(file.FileName);

                var imgLarge = Images.CreateThumbnailToImage(file.InputStream, TypePathImage.Large);
                var imgSmall = Images.CreateThumbnailToImage(file.InputStream, TypePathImage.Small);
                var imgThumb = Images.CreateThumbnailToImage(file.InputStream, TypePathImage.Thumbnail);

                img.FullHdPath = StaticHelper.SaveImage(path, TypePathImage.Large, imgLarge, fileNameExpected, extension);
                img.SmallPath = StaticHelper.SaveImage(path, TypePathImage.Small, imgSmall, fileNameExpected, extension);
                img.ThumpnailPath = StaticHelper.SaveImage(path, TypePathImage.Thumbnail, imgThumb, fileNameExpected, extension);
                if (service == null)
                    service = new CMS.WCFService.ServicesServer.ServerService();
                service.SaveImage(img);
            }

        }

        //
        // GET: /Admin/ImageAdmin/Edit/5
        public ActionResult Edit(Guid id)
        {
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        //
        // POST: /Admin/ImageAdmin/Edit/5

        [HttpPost]
        public ActionResult Edit(Image image)
        {
            if (ModelState.IsValid)
            {
                if (image.Picture != null)
                {
                    string path = "/ImageRepoisitory/images/" + image.Discriminator + "/" + image.GuidId;
                    string fileNameExpected = "_" + image.Name + "_ImageDetail";
                    string extension = System.IO.Path.GetExtension(image.Picture.FileName);

                    var imgLarge = Images.CreateThumbnailToImage(image.Picture.InputStream, TypePathImage.Large);
                    var imgSmall = Images.CreateThumbnailToImage(image.Picture.InputStream, TypePathImage.Small);
                    var imgThumb = Images.CreateThumbnailToImage(image.Picture.InputStream, TypePathImage.Thumbnail);

                    image.FullHdPath = StaticHelper.SaveImage(path, TypePathImage.Large, imgLarge, fileNameExpected, extension);
                    image.SmallPath = StaticHelper.SaveImage(path, TypePathImage.Small, imgSmall, fileNameExpected, extension);
                    image.ThumpnailPath = StaticHelper.SaveImage(path, TypePathImage.Thumbnail, imgThumb, fileNameExpected, extension);
                }
                CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
                service.SaveImage(image);
                return RedirectToAction("ImageByDiscriminator", new { Discriminator = image.Discriminator, IdBelong = image.IdBelong });
            }
            return View(image);
        }

        //
        // GET: /Admin/ImageAdmin/Delete/5

        public ActionResult Delete(Guid id)
        {
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        //
        // POST: /Admin/ImageAdmin/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Image image = db.Images.Find(id);
            db.Images.Remove(image);
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