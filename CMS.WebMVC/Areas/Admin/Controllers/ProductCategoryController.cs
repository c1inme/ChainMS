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
    public class ProductCategoryController : Controller
    {
        private DBServerContext db = new DBServerContext();
        CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
        //
        // GET: /Admin/ProductCategory/

        public ActionResult Index()
        {
            var ProductCategorys = service.GetProductCategory(null);
            return View(ProductCategorys.ToList());
        }

        public ActionResult AddProperty(Guid id)
        {
            var fullProductCategory = service.GetProductCategoryFullPropertiesByKey(id);
            var AllProperties = service.GetAllPropertiesDef();
            List<AddPropertyModels> result = new List<AddPropertyModels>();
            foreach (var item in AllProperties)
                result.Add(new AddPropertyModels()
                {
                    IsCheck = fullProductCategory.ListProperty == null ? false : fullProductCategory.ListProperty.Contains(item),
                    Dicriminator = "ProductCategory",
                    IdBelong = id,
                    ProperDef = item,
                    GuidProperty = item.GuidId
                });
            return View(result.OrderByDescending(f => f.IsCheck));
        }


        //
        // POST: /Admin/ProductCategory/Create
        [HttpPost]
        public ActionResult CreateProperty(IEnumerable<AddPropertyModels> listToAdd)
        {
            if (ModelState.IsValid)
            {
                var getAllChecked = listToAdd.Where(f => f.IsCheck).ToList();
                var fullProductCategory = service.GetProductCategoryFullPropertiesByKey(listToAdd.FirstOrDefault().IdBelong);
                if (fullProductCategory.ListProperty != null)
                {
                    var propertyDeleted = fullProductCategory.ListProperty.Where(f => getAllChecked.Count(x => x.GuidProperty == f.GuidId) == 0);
                    service.DeleteRelationOfProperties(propertyDeleted.Select(f => f.GuidId).ToList(), listToAdd.FirstOrDefault().IdBelong, "ProductCategory");
                    var propertyAdded = getAllChecked.Where(f => fullProductCategory.ListProperty.Count(x => x.GuidId == f.GuidProperty) == 0);
                    Dictionary<Guid, string> toAdd = new Dictionary<Guid, string>();
                    foreach (var item in propertyAdded)
                        toAdd.Add(item.GuidProperty, item.ValueProperty);
                    service.AddRelationOfProperties(toAdd, listToAdd.FirstOrDefault().IdBelong, "ProductCategory");

                }
                else
                {
                    Dictionary<Guid, string> toAdd = new Dictionary<Guid, string>();
                    foreach (var item in getAllChecked)
                        toAdd.Add(item.GuidProperty, item.ValueProperty);
                    service.AddRelationOfProperties(toAdd, listToAdd.FirstOrDefault().IdBelong, "ProductCategory");
                }
                //service.SaveProductCategory(productcategory, false);
            }
            return RedirectToAction("Index");
            //ViewBag.ParentId = new SelectList(db.ProductCategorys, "GuidId", "Name", productcategory.ParentId);
            //return View(listToAdd);
        }


        //
        // GET: /Admin/ProductCategory/Details/5
        public ActionResult Details(Guid id)
        {
            ProductCategory productcategory = db.ProductCategorys.Find(id);
            if (productcategory == null)
            {
                return HttpNotFound();
            }
            return View(productcategory);
        }

        //
        // GET: /Admin/ProductCategory/Create

        public ActionResult Create(string guidParent = null)
        {
            if (guidParent == null)
                guidParent = Guid.Empty.ToString();
            CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
            List<ProductCategory> lstParent = service.GetProductCategory(null);
            lstParent.Insert(0, new ProductCategory() { GuidId = Guid.Empty, Name = "None" });
            ViewBag.ListProductCategory = lstParent;
            ProductCategory forCreate = new ProductCategory();
            forCreate.ParentId = new Guid(guidParent);
            return View(forCreate);
        }

        //
        // POST: /Admin/ProductCategory/Create
        [HttpPost]
        public ActionResult Create(ProductCategory productcategory)
        {
            if (ModelState.IsValid)
            {
                if (productcategory.Picture != null)
                {
                    string path = "/ImageRepoisitory/productcategory/" + productcategory.GuidId;
                    string fileNameExpected = "_" + productcategory.Name + "_productcategory";
                    productcategory.IconImage = StaticHelper.SaveFileImage(path, productcategory.Picture, fileNameExpected);
                }
                service.SaveProductCategory(productcategory, false);
                return RedirectToAction("Index");
            }

            //ViewBag.ParentId = new SelectList(db.ProductCategorys, "GuidId", "Name", productcategory.ParentId);
            return View(productcategory);
        }

        //
        // GET: /Admin/ProductCategory/Edit/5

        public ActionResult Edit(Guid id)
        {
            CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
            ProductCategory productcategory = service.GetProductCategoryByKey(id);
            if (productcategory == null)
            {
                return HttpNotFound();
            }
            List<MenuCategory> lstParent = service.GetMenuCategory(null);
            lstParent.Insert(0, new MenuCategory() { GuidId = Guid.Empty, Name = "None" });
            ViewBag.ListProductcategory = lstParent;
            return View(productcategory);
        }

        //
        // POST: /Admin/ProductCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(ProductCategory productcategory)
        {
            if (ModelState.IsValid)
            {
                if (productcategory.Picture != null)
                {
                    string path = "/ImageRepoisitory/productcategory/" + productcategory.GuidId;
                    string fileNameExpected = "_" + productcategory.Name + "_productcategory";
                    productcategory.IconImage = StaticHelper.SaveFileImage(path, productcategory.Picture, fileNameExpected);
                }
                productcategory = service.SaveProductCategory(productcategory, false);
                return RedirectToAction("Index");
            }
            // ViewBag.ParentId = new SelectList(db.ProductCategorys, "GuidId", "Name", productcategory.ParentId);
            return View(productcategory);
        }

        //
        // GET: /Admin/ProductCategory/Delete/5

        public ActionResult Delete(Guid id)
        {
            ProductCategory productcategory = db.ProductCategorys.Find(id);
            if (productcategory == null)
            {
                return HttpNotFound();
            }
            return View(productcategory);
        }

        //
        // POST: /Admin/ProductCategory/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ProductCategory productcategory = db.ProductCategorys.Find(id);
            db.ProductCategorys.Remove(productcategory);
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