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
    public class ProductController : Controller
    {
        private DBServerContext db = new DBServerContext();
        CMS.WCFService.ServicesServer.ServerService service = new CMS.WCFService.ServicesServer.ServerService();
        //
        // GET: /Admin/Product/
        public ActionResult Index(Guid? id = null)
        {


            List<ProductCategory> lstParent = service.GetProductCategory(null);
            lstParent.Insert(0, new ProductCategory() { GuidId = Guid.Empty, Name = "None" });
            ViewBag.ListProductCategory = lstParent;
            var products = new List<Product>();
            if (id == null)
                products = service.GetProduct(null);
            else
            {
                products = service.GetProductByCategory(string.Empty, id.Value, true);
            }
            return View(products.ToList());
        }

        //
        // GET: /Admin/Product/Details/5
        public ActionResult Details(Guid id)
        {
            var product = service.GetProductFullPropertiesByKey(id);
             List<AddPropertyModels> result = new List<AddPropertyModels>();
             if (product.ListProperty != null)
             {
                 foreach (var item in product.ListProperty)
                     result.Add(new AddPropertyModels()
                     {
                         Dicriminator = "Product",
                         IdBelong = id,
                         ProperDef = item,
                         GuidProperty = item.GuidId,
                         ValueProperty = product.ListRelationOfProperties == null ? "" : product.ListRelationOfProperties.FirstOrDefault(f => f.IdProperty == item.GuidId) == null ? "" : product.ListRelationOfProperties.FirstOrDefault(f => f.IdProperty == item.GuidId).VaueProperty
                     });
             }
            ViewBag.AddPropertyModels = result;
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }



        public ActionResult ManageProperty(Guid id)
        {
            var fullProductCategory = service.GetProductFullPropertiesByKey(id);
            var AllProperties = service.GetAllPropertiesDef();
            List<AddPropertyModels> result = new List<AddPropertyModels>();
            foreach (var item in AllProperties)
                result.Add(new AddPropertyModels()
                {
                    IsCheck = fullProductCategory.ListProperty == null ? false : fullProductCategory.ListProperty.Contains(item),
                    Dicriminator = "Product",
                    IdBelong = id,
                    ProperDef = item,
                    GuidProperty = item.GuidId,
                    ValueProperty = fullProductCategory.ListRelationOfProperties == null ? "" : fullProductCategory.ListRelationOfProperties.FirstOrDefault(f=>f.IdProperty == item.GuidId) == null ? ""  : fullProductCategory.ListRelationOfProperties.FirstOrDefault(f=>f.IdProperty == item.GuidId).VaueProperty
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
                var fullProductCategory = service.GetProductFullPropertiesByKey(listToAdd.FirstOrDefault().IdBelong);
                if (fullProductCategory.ListProperty != null)
                {
                    var propertyDeleted = fullProductCategory.ListProperty.Where(f => getAllChecked.Count(x => x.GuidProperty == f.GuidId) == 0);
                    service.DeleteRelationOfProperties(propertyDeleted.Select(f => f.GuidId).ToList(), listToAdd.FirstOrDefault().IdBelong, "Product");
                    var propertyAdded = getAllChecked.Where(f => propertyDeleted.Count(x => x.GuidId == f.GuidProperty) == 0);
                    Dictionary<Guid, string> toAdd = new Dictionary<Guid, string>();
                    foreach (var item in propertyAdded)
                        toAdd.Add(item.GuidProperty, item.ValueProperty);
                    service.AddRelationOfProperties(toAdd, listToAdd.FirstOrDefault().IdBelong, "Product");
                }
                else
                {
                    Dictionary<Guid, string> toAdd = new Dictionary<Guid, string>();
                    foreach (var item in getAllChecked)
                        toAdd.Add(item.GuidProperty, item.ValueProperty);
                    service.AddRelationOfProperties(toAdd, listToAdd.FirstOrDefault().IdBelong, "Product");
                }
                //service.SaveProductCategory(productcategory, false);
            }
            return RedirectToAction("Index");
            //ViewBag.ParentId = new SelectList(db.ProductCategorys, "GuidId", "Name", productcategory.ParentId);
            //return View(listToAdd);
        }



        //
        // GET: /Admin/Product/Create

        public ActionResult Create()
        {
            List<ProductCategory> lstParent = service.GetProductCategory(null);

            ViewBag.ListProductCategory = lstParent;
            ViewBag.ManufactureId = new SelectList(db.Manufactures, "GuidId", "Name");
            return View();
        }

        //
        // POST: /Admin/Product/Create

        [HttpPost]
        public ActionResult Create(Product product)
        {
            product.SellPrice = Convert.ToDecimal(product.SellPrice);
            if (ModelState.IsValid)
            {
                if (product.Picture != null)
                {
                    string path = "/ImageRepoisitory/product/" + product.GuidId;
                    string fileNameExpected = "_" + product.Name + "_product";
                    product.ImagePath = StaticHelper.SaveFileImage(path, product.Picture, fileNameExpected);
                    ImageAdminController.CreateImageRelation(product.Picture, product.GuidId, "Product", service);
                }
                service.SaveProduct(product, false);
                return RedirectToAction("Index");
            }
            List<ProductCategory> lstParent = service.GetProductCategory(null);
            ViewBag.ListProductCategory = lstParent;
            ViewBag.ManufactureId = new SelectList(db.Manufactures, "GuidId", "Name", product.ManufactureId);
            return View(product);
        }

        //
        // GET: /Admin/Product/Edit/5

        public ActionResult Edit(Guid id)
        {
            Product product = service.GetProductByKey(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            List<ProductCategory> lstParent = service.GetProductCategory(null);
            ViewBag.ListProductCategory = lstParent;
            ViewBag.ManufactureId = new SelectList(db.Manufactures, "GuidId", "Name", product.ManufactureId);
            return View(product);
        }

        //
        // POST: /Admin/Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Picture != null)
                {
                    string path = "/ImageRepoisitory/product/" + product.GuidId;
                    string fileNameExpected = "_" + product.Name + "_product";
                    product.ImagePath = StaticHelper.SaveFileImage(path, product.Picture, fileNameExpected);
                    ImageAdminController.CreateImageRelation(product.Picture, product.GuidId, "Product", service);
                }
                service.SaveProduct(product, false);
                return RedirectToAction("Index");
            }
            List<ProductCategory> lstParent = service.GetProductCategory(null);
            ViewBag.ListProductCategory = lstParent;
            ViewBag.ManufactureId = new SelectList(db.Manufactures, "GuidId", "Name", product.ManufactureId);
            return View(product);
        }

        //
        // GET: /Admin/Product/Delete/5

        public ActionResult Delete(Guid id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Admin/Product/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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