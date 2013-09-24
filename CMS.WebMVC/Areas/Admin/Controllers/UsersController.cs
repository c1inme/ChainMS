using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CMS.Entities.ServerObjects;

namespace CMS.WebMVC.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private DBServerContext db = new DBServerContext();

        //
        // GET: /Admin/Users/

        public ActionResult Index()
        {
            CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
            var result = service.GetUsers(null).OrderByDescending(f => f.ModifyDate);
            return View(result);
        }

        //
        // GET: /Admin/Users/Details/5

        public ActionResult Details(Guid? id = null)
        {
            Users users = db.Userss.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        //
        // GET: /Admin/Users/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Users/Create

        [HttpPost]
        public ActionResult Create(Users users)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus createStatus = default(MembershipCreateStatus);
                Membership.CreateUser(users.Alias, users.Password, users.Alias, null, null, users.IsApproved, users.GuidId, out createStatus);
                if (users.Avartar != null)
                {
                    string path = "/ImageRepoisitory/Users/" + users.GuidId;
                    string fileNameExpected = "_" + users.FullName + "_Avartar";
                    users.ImagePath = StaticHelper.SaveFileImage(path, users.Avartar,fileNameExpected );
                }

                //if(!System.IO.File.Exists(pathFolder))
                //    System.IO.File.Create(pathFolder+"\\"+users.Avartar.SaveAs(
                if (createStatus == MembershipCreateStatus.Success)
                {
                    CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
                    var userRegisted = service.GetUserByKey(users.GuidId);
                    userRegisted.FirstName = users.FirstName;
                    userRegisted.LastName = users.LastName;
                    userRegisted.ImagePath = users.ImagePath;
                    //userRegisted.IsApproved = users.IsApproved;
                    userRegisted.IsSystem = users.IsSystem;
                    userRegisted.MobileUser = users.MobileUser;
                    service.SaveUser(userRegisted, true);
                }
                else
                {
                    ModelState.AddModelError("", createStatus.ToString());
                    return null;
                }

                return RedirectToAction("Index");
            }

            return View(users);
        }

        //
        // GET: /Admin/Users/Edit/5

        public ActionResult Edit(Guid? id = null)
        {
            Users users = db.Userss.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        //
        // POST: /Admin/Users/Edit/5

        [HttpPost]
        public ActionResult Edit(Users users)
        {
            if (ModelState.IsValid)
            {
                if (users.Avartar != null)
                {
                    string path = "/ImageRepoisitory/Users/" + users.GuidId;
                    string fileNameExpected = "_" + users.FullName + "_Avartar";
                    users.ImagePath = StaticHelper.SaveFileImage(path, users.Avartar, fileNameExpected);
                }
                CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
                service.SaveUser(users, false);
                return RedirectToAction("Index");
            }
            return View(users);
        }

        //
        // GET: /Admin/Users/Delete/5

        public ActionResult Delete(Guid? id = null)
        {
            Users users = db.Userss.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        //
        // POST: /Admin/Users/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Users users = db.Userss.Find(id);
            db.Userss.Remove(users);
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