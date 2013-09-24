using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CMS.BussinessLayer.Servers.Base;
using CMS.Entities;
using CMS.Entities.ServerObjects;
using CMS.Kernel;
namespace CMS.BussinessLayer.Servers
{
    public class BusinessServerService : BussinessServiceServerBase
    {
        #region Constructor methods
        public BusinessServerService(DbContextRepository ct)
            : base(ct)
        {
        }
        #endregion

        #region News region
        public void SaveNews(News newsToCreate)
        {
            //GenericRepository peopleRepository = new PersonRepository();
            db.SaveEntity(newsToCreate);
        }

        public IEnumerable<News> GetNews()
        {

            return db.Find<News>();
        }

        public News GetNewByKey(Guid key)
        {
            return db.FindFirst<News>(f => f.GuidId == key, StaticMethods.CreateInclude<News>(a => a.MenuCategory), false);
        }
        public News GetByID(long id)
        {
            return db.FindFirst<News>(f => f.EntityId == id);
        }

        public string DeleteNews(object id, bool isSoftDeleted = true)
        {
            try
            {
                var entityDelete = db.FindByKey<News>(id);
                db.Delete(entityDelete, isSoftDeleted);
                return "Success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion

        #region Users Region
        public List<Users> GetUsers(Expression<Func<Users, bool>> where)
        {
            return db.Find(where).ToList();
        }


        public Users GetUserByKey(Guid key)
        {
            return db.FindByKey<Users>(key);
        }


        public void SaveUser(Users user, bool isSkipversion)
        {
            db.SaveEntity(user, false, isSkipversion);
        }

        public void DeleteUser(Guid guiId)
        {
            db.DeleteByKey<Users>(guiId);
        }
        #endregion

        #region MenuCategory


        public MenuCategory GetMenuCategoryByKey(Guid key)
        {
            var result = db.FindFirst<MenuCategory>(f => f.GuidId == key, StaticMethods.CreateInclude<MenuCategory>(a => a.ParentMenuCategory), false);
            return result;
        }

        public List<MenuCategory> GetMenuCategory(Expression<Func<MenuCategory, bool>> where)
        {
            var listAll = db.Find(where, StaticMethods.CreateInclude<MenuCategory>(f => f.ParentMenuCategory, f => f.Lang)).ToList();
            var result = new List<MenuCategory>(listAll.Where(f => f.ParentId == null));
            var listToRef = new List<MenuCategory>(listAll.Except(result));
            BuildHierarchical(result, listToRef);
            return result;
        }


        private void BuildHierarchical(List<MenuCategory> listItem, List<MenuCategory> listToRef)
        {
            if (listItem.Count() == 0)
                return;

            foreach (var item in listItem)
            {
                item.ListMenuCategory = new List<MenuCategory>();
                item.ListMenuCategory.AddRange(listToRef.Where(f => f.ParentId == item.GuidId).ToList());
                var listRef = listToRef.Except(item.ListMenuCategory.ToList()).ToList();
                if (listToRef.Count > 0)
                    BuildHierarchical(item.ListMenuCategory.ToList(), listRef);
            }
        }

        public void SaveMenuCategory(MenuCategory entity, bool isSkipversion)
        {
            db.SaveEntity(entity, false, isSkipversion);
        }
        public void DeleteMenuCategoryByKey(Guid key)
        {
            db.DeleteByKey<MenuCategory>(key);
        }
        #endregion

        #region ProductCategory
        public ProductCategory GetProductCategoryByKey(Guid key)
        {
            var result = db.FindFirst<ProductCategory>(f => f.GuidId == key, StaticMethods.CreateInclude<ProductCategory>(a => a.ParentProductCategory), false);
            return result;
        }
        public ProductCategory GetProductCategoryFullPropertiesByKey(Guid key)
        {
            var result = db.FindFirst<ProductCategory>(f => f.GuidId == key, StaticMethods.CreateInclude<ProductCategory>(a => a.ParentProductCategory), false);
            if (result != null)
            {
                result.ListRelationOfProperties = db.Find<RelationOfProperties>(x => x.IdBelong == result.GuidId && x.Dicriminator == "ProductCategory", StaticMethods.CreateInclude<RelationOfProperties>(f => f.PropertyDef)).ToList();
                if (result.ListRelationOfProperties != null)
                {
                    result.ListProperty = new List<PropertiesDefinition>();
                    foreach (var item in result.ListRelationOfProperties)
                    {
                        if (result.ListProperty.Count(f => f.GuidId == item.IdProperty) == 0)
                            result.ListProperty.Add(item.PropertyDef);
                    }
                }
            }
            return result;
        }

        public List<ProductCategory> GetProductCategory(Expression<Func<ProductCategory, bool>> where)
        {
            var listAll = db.Find(where, StaticMethods.CreateInclude<ProductCategory>(f => f.ParentProductCategory)).ToList();
            var result = new List<ProductCategory>(listAll.Where(f => f.ParentId == null));
            var listToRef = new List<ProductCategory>(listAll.Except(result));
            BuildProductCategoryHierarchical(result, ref listToRef);
            return result;
        }


        private void BuildProductCategoryHierarchical(List<ProductCategory> listItem, ref List<ProductCategory> listToRef)
        {
            if (listItem.Count() == 0)
                return;

            foreach (var item in listItem)
            {
                item.ListProductCategory = new List<ProductCategory>();
                item.ListProductCategory = listToRef.Where(f => f.ParentId == item.GuidId).ToList();
                var listRef = listToRef.Except(item.ListProductCategory.ToList()).ToList();
                listToRef = listRef;
                if (listToRef.Count > 0)
                    BuildProductCategoryHierarchical(item.ListProductCategory.ToList(), ref listToRef);
            }
        }

        public void SaveProductCategory(ProductCategory entity, bool isSkipversion)
        {
            db.SaveEntity(entity, false, isSkipversion);
        }
        public void DeleteProductCategoryByKey(Guid key)
        {
            db.DeleteByKey<ProductCategory>(key);
        }
        #endregion

        #region Product


        public Product GetProductFullPropertiesByKey(Guid key)
        {
            var result = db.FindFirst<Product>(f => f.GuidId == key, StaticMethods.CreateInclude<Product>(a => a.ProductCategory, a => a.Manufacture), false);
            if (result != null)
            {
                result.ListRelationOfProperties = db.Find<RelationOfProperties>(x => x.IdBelong == result.GuidId && x.Dicriminator == "Product", StaticMethods.CreateInclude<RelationOfProperties>(f => f.PropertyDef)).ToList();
                if (result.ListRelationOfProperties != null)
                {
                    result.ListProperty = new List<PropertiesDefinition>();
                    foreach (var item in result.ListRelationOfProperties)
                    {
                        if (result.ListProperty.Count(f => f.GuidId == item.IdProperty) == 0)
                            result.ListProperty.Add(item.PropertyDef);
                    }
                }
                result.ListImage = db.Find<Image>(x => x.IdBelong == result.GuidId && x.Discriminator == "Product").ToList();
                result.ListNews = db.Find<News>(x => x.IdBelong == result.GuidId && x.Discriminator == "Product").ToList();
            }
            return result;
        }
        public Product GetProductByKey(Guid key)
        {
            var result = db.FindFirst<Product>(f => f.GuidId == key, StaticMethods.CreateInclude<Product>(a => a.ProductCategory, a => a.Manufacture), false);
            return result;
        }

        public List<Product> GetProduct(Expression<Func<Product, bool>> where)
        {
            var listAll = db.Find(where, StaticMethods.CreateInclude<Product>(a => a.ProductCategory, a => a.Manufacture), false).ToList();
            return listAll;
        }

        public List<Product> GetProductByCategory(string keyWord, Guid? categoryGuid = null, bool isSubCategory = true)
        {
            List<Product> result = new List<Product>();
            if (categoryGuid == null)
                result = db.Find(null, StaticMethods.CreateInclude<Product>(a => a.ProductCategory, a => a.Manufacture), false).ToList();
            var categoryRoot = db.Find<ProductCategory>(f => f.GuidId == categoryGuid.Value).ToList();

            var allCategory = db.Find<ProductCategory>(null, StaticMethods.CreateInclude<ProductCategory>(f => f.ParentProductCategory), false).ToList();
            var cloneAll = new List<ProductCategory>(allCategory);
            BuildProductCategoryHierarchical(categoryRoot, ref allCategory);
            var subCategory = cloneAll.Except(allCategory).ToList();
            IQueryable<Product> q = db.Select<Product>();
            foreach (var cate in subCategory)
            {
                if (!string.IsNullOrEmpty(keyWord))
                {
                    result.AddRange(db.Find<Product>(x => x.ProductCategoryId == cate.GuidId &&
                        (x.Name.ToLower().Contains(keyWord.ToLower()) || x.Code.ToLower().Contains(keyWord.ToLower()))
                        ));
                }
                else
                {
                    result.AddRange(db.Find<Product>(f => f.ProductCategoryId == cate.GuidId));
                }
            }
            return result;
        }

        public void SaveProduct(Product entity, bool isSkipversion)
        {
            db.SaveEntity(entity, false, isSkipversion);
        }
        public void DeleteProductByKey(Guid key)
        {
            db.DeleteByKey<Product>(key);
        }
        #endregion

        #region PropertiesDefinition
        public List<PropertiesDefinition> GetAllPropertiesDef()
        {
            return db.Find<PropertiesDefinition>(null, null).ToList();
        }
        #endregion

        #region PropertiesDefinition
        public void AddRelationOfProperties(Dictionary<Guid, string> propertyGuids, Guid guidBelong, string dicriminator)
        {
            var propeties = db.Find<RelationOfProperties>(f => f.IdBelong == guidBelong && f.Dicriminator == dicriminator, null, true);
            var properiesDeleted = propeties.Where(f => propertyGuids.Count(x => x.Key == f.IdProperty) > 0);
            //re-use properties has been deleted
            foreach (var item in properiesDeleted)
            {
                item.Deleted = false;
                KeyValuePair<Guid, string>? valuez = propertyGuids.FirstOrDefault(f => f.Key == item.IdProperty);
                item.VaueProperty = valuez == null ? null : valuez.Value.Value;
            }

            //For new property
            var needToAdd = propertyGuids.Where(f => properiesDeleted.Count(x => x.IdProperty == f.Key) == 0).ToList();
            var listoAdd = new List<RelationOfProperties>();
            foreach (var item in needToAdd)
            {
                if (properiesDeleted.Count(f => f.IdProperty == item.Key) == 0)
                    listoAdd.Add(new RelationOfProperties()
                    {
                        IdBelong = guidBelong,
                        Dicriminator = dicriminator,
                        IdProperty = item.Key,
                        VaueProperty = item.Value
                    });

            }
            db.SaveEntities(properiesDeleted);
            db.SaveEntities(listoAdd);
        }

        public void DeleteRelationOfProperties(List<Guid> propertyGuids, Guid guidCategory, string dicriminator)
        {
            var propeties = db.Find<RelationOfProperties>(f => f.IdBelong == guidCategory).ToList();
            var toDelete = propeties.Where(f => propertyGuids.Count(x => x == f.IdProperty) > 0);
            db.DeleteEntities(toDelete);
        }
        #endregion

        #region Function for Manufacture
        public List<Manufacture> GetManufacture(Expression<Func<Manufacture, bool>> where)
        {
            var result = db.Find(where).ToList();
            return result;
        }

        public Manufacture GetManufactureByKey(Guid key)
        {
            var result = db.FindFirst<Manufacture>(f => f.GuidId == key, null, false);
            return result;
        }

        public void SaveManufacture(Manufacture obj)
        {
            db.SaveEntity(obj, false);
        }

        public void DeleteManufactureByKey(Guid key)
        {
            db.DeleteByKey<Manufacture>(key);
        }

        #endregion

        #region Function for Gallery
        public List<Gallery> GetGallery(Expression<Func<Gallery, bool>> where)
        {
            var result = db.Find(where).ToList();

            return result;
        }

        public Gallery GetGalleryByKey(Guid key)
        {
            var result = db.FindFirst<Gallery>(f => f.GuidId == key, null, false);
            result.ListImage = db.Find<Image>(f => f.Discriminator == "Gallery" && f.IdBelong == f.GuidId).ToList();
            return result;
        }

        public void SaveGallery(Gallery obj)
        {
            db.SaveEntity(obj, false);
        }

        public void DeleteGalleryByKey(Guid key)
        {
            db.DeleteByKey<Gallery>(key);
        }

        #endregion

        #region Function for Image
        public List<Image> GetImage(Expression<Func<Image, bool>> where)
        {
            var result = db.Find(where).ToList();

            return result;
        }

        public Image GetImageByKey(Guid key)
        {
            var result = db.FindFirst<Image>(f => f.GuidId == key, null, false);
            return result;
        }

        public void SaveImage(Image obj)
        {
            var imgExist = GetImageByKey(obj.GuidId);
            if (imgExist != null)
            {
                //Assign new path for Image
                imgExist.FullHdPath = obj.FullHdPath;
                imgExist.SmallPath = obj.SmallPath;
                imgExist.ThumpnailPath = obj.ThumpnailPath;
                obj = imgExist;
            }
            db.SaveEntity(obj, false);
        }

        public void DeleteImageByKey(Guid key)
        {
            db.DeleteByKey<Image>(key);
        }

        #endregion

        #region Function for Rating
        public List<Rating> GetRating(Expression<Func<Rating, bool>> where)
        {
            var result = db.Find(where).ToList();

            return result;
        }

        public Rating GetRatingByKey(Guid key)
        {
            var result = db.FindFirst<Rating>(f => f.GuidId == key, null, false);
            return result;
        }

        public void SaveRating(Rating obj)
        {
            db.SaveEntity(obj, false);
        }

        public void DeleteRatingByKey(Guid key)
        {
            db.DeleteByKey<Rating>(key);
        }

        #endregion

        #region Function for Comment
        public List<Comment> GetComment(Expression<Func<Comment, bool>> where)
        {
            var result = db.Find(where).ToList();

            return result;
        }

        public Comment GetCommentByKey(Guid key)
        {
            var result = db.FindFirst<Comment>(f => f.GuidId == key, null, false);
            return result;
        }

        public void SaveComment(Comment obj)
        {
            db.SaveEntity(obj, false);
        }

        public void DeleteCommentByKey(Guid key)
        {
            db.DeleteByKey<Comment>(key);
        }

        #endregion


        #region Get category base
        public CategoryCollection GetCategoryForIndexMenu(string codeLang)
        {
            CategoryCollection result = new CategoryCollection();
            var menuCategorys = GetMenuCategory(f => f.Lang.CodeLanguage == codeLang);

            BaseCategory baseCategory;
            foreach (var item in menuCategorys)
            {
                baseCategory = new BaseCategory();
                HelperLibrary.ConvertHelper.ConvertEntity(item, baseCategory);
                if (item.ListMenuCategory != null && item.ListMenuCategory.Count > 0)
                    BuildSubCategory(baseCategory, item.ListMenuCategory);
                result.Add(baseCategory);

            }

            var productCategorys = GetProductCategory(f => f.Lang.CodeLanguage == codeLang);
            foreach (var item in productCategorys)
            {
                baseCategory = new BaseCategory();
                HelperLibrary.ConvertHelper.ConvertEntity(item, baseCategory);
                if (item.ListProductCategory != null && item.ListProductCategory.Count > 0)
                    BuildSubProductCategory(baseCategory, item.ListProductCategory);
                result.Add(baseCategory);
            }

            return result;
        }
        private void BuildSubProductCategory(BaseCategory baseCategory, List<ProductCategory> menus)
        {
            BaseCategory baseSubCategory = null;
            foreach (var item in menus)
            {
                baseSubCategory = new BaseCategory();
                HelperLibrary.ConvertHelper.ConvertEntity(item, baseSubCategory);
                if (item.ListProductCategory != null && item.ListProductCategory.Count > 0)
                    BuildSubProductCategory(baseSubCategory, item.ListProductCategory);

                if (baseCategory.ListBaseCategory == null)
                    baseCategory.ListBaseCategory = new CategoryCollection();
                baseCategory.ListBaseCategory.Add(baseSubCategory);
            }
        }

        private void BuildSubCategory(BaseCategory baseCategory, List<MenuCategory> menus)
        {
            BaseCategory baseSubCategory = null;
            foreach (var item in menus)
            {
                baseSubCategory = new BaseCategory();
                HelperLibrary.ConvertHelper.ConvertEntity(item, baseSubCategory);
                if (item.ListMenuCategory != null && item.ListMenuCategory.Count > 0)
                    BuildSubCategory(baseSubCategory, item.ListMenuCategory);

                if (baseCategory.ListBaseCategory == null)
                    baseCategory.ListBaseCategory = new CategoryCollection();
                baseCategory.ListBaseCategory.Add(baseSubCategory);
            }
        }
        #endregion

        #region Function for Language
        public List<Language> GetLanguage(Expression<Func<Language, bool>> where)
        {
            var result = db.Find(where).OrderBy(f => f.OrderNumber).ToList();
            return result;
        }

        public Language GetLanguageByKey(Guid key)
        {
            var result = db.FindFirst<Language>(f => f.GuidId == key, null, false);
            return result;
        }

        public void SaveLanguage(Language obj)
        {
            db.SaveEntity(obj, false);
        }

        public void DeleteLanguageByKey(Guid key)
        {
            db.DeleteByKey<Language>(key);
        }

        #endregion

    }
}
