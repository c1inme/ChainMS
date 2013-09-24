using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.WebMVC.Controllers
{
    public class ImageHelperController : Controller
    {
        //
        // GET: /ImageHelper/

        public ActionResult Index()
        {
            return View();
        }



        #region Ajax Submit
        [AcceptVerbs(HttpVerbs.Post)]
        //[HttpPost]
        public ActionResult AjaxSubmit(long? id = 0)
        {
           
            Session["Image.ContentLength"] = Request.Files[0].ContentLength;
            if (Request.Files[0].ContentLength == 0)
                return Content("");
            Session["Image.ContentType"] = Request.Files[0].ContentType;
            byte[] b = new byte[Request.Files[0].ContentLength];
            Request.Files[0].InputStream.Read(b, 0, Request.Files[0].ContentLength);
            Session["Image.ContentStream"] = b;
            if (id > 0)
            {
                byte[] thumbnail = Images.CreateThumbnailToByte(Request.Files[0].InputStream, 100, 100);
                Session["Thumbnail.ContentLength"] = thumbnail.Length;
                Session["Thumbnail.ContentType"] = Request.Files[0].ContentType;
                byte[] c = new byte[thumbnail.Length];
                Request.Files[0].InputStream.Read(c, 0, Request.Files[0].ContentLength);
                Session["Thumbnail.ContentStream"] = thumbnail;
            }
            return Content(Request.Files[0].ContentType + ";" + Request.Files[0].ContentLength);
        }


        #endregion

        #region ImageLoad
        public ActionResult ImageLoad(long? id = null)
        {
            if (!id.HasValue)
                return Content("");
            int length = (int)Session["Image.ContentLength"];
            if (length == 0)
                return Content("");
            if (id == 0)
            {
                byte[] b = (byte[])Session["Image.ContentStream"];
                string type = (string)Session["Image.ContentType"];
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = type;
                Response.BinaryWrite(b);
                Response.Flush();
                Response.End();
                Session["Image.ContentLength"] = null;
                Session["Image.ContentType"] = null;
                Session["Image.ContentStream"] = null;
            }

            //--The following is the Thumnbail id.
            if (id == 1)
            {
                byte[] b = (byte[])Session["Thumbnail.ContentStream"];
                string type = (string)Session["Thumbnail.ContentType"];
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = type;
                Response.BinaryWrite(b);
                Response.Flush();
                Response.End();
                Session["Thumbnail.ContentLength"] = null;
                Session["Thumbnail.ContentType"] = null;
                Session["Thumbnail.ContentStream"] = null;
            }
            return Content("");
        }
        #endregion

    }
}
