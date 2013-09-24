using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CMS.BussinessLayer.Servers;
using CMS.Entities;
using CMS.Entities.ServerObjects;
using CMS.Kernel;

namespace CMS.WCFService.ServicesServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServerService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServerService.svc or ServerService.svc.cs at the Solution Explorer and start debugging.
    [ServiceContract]
    public class ServerService
    {


        #region News
        [OperationContract]
        public IEnumerable<News> GetNews()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>(true))
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    return generalService.GetNews();
                }
            }
            catch (Exception ex)
            {
                //Todo Log
                return null;
            }
        }

        [OperationContract]
        public News GetNewByKey(Guid Id)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    return generalService.GetNewByKey(Id);
                }
            }
            catch
            {
                //Todo Logs
                return null;
            }
        }

        [OperationContract]
        public News GetNewByID(int Id)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    return generalService.GetByID(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //Todo Logs
            }
        }

        [OperationContract]
        public News SaveNews(News entity)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {

                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    generalService.SaveNews(entity);
                    unitOfWork.Comit();
                    return generalService.GetByID(entity.EntityId);


                }
            }
            catch (Exception ex)
            {
                throw ex;
                //Todo log file
            }
        }


        [OperationContract]
        public string DeleteNews(int entityID, bool isSoftDeleted)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {

                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    var result = generalService.DeleteNews(entityID, isSoftDeleted);
                    unitOfWork.Comit();
                    return result;

                }
            }
            catch (Exception ex)
            {
                //Todo log file
                return ex.Message;
            }
        }

    
        #endregion

        #region Users

        [OperationContract]
        public List<Users> GetUsers(Expression<Func<Users, bool>> where)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    var result = generalService.GetUsers(where);
                    return result;
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        [OperationContract]
        public Users GetUserByKey(Guid key)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    var user = generalService.GetUserByKey(key);
                    return user;
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }


        [OperationContract]
        public Users SaveUser(Users user, bool isSkipVersion)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {

                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    generalService.SaveUser(user, isSkipVersion);
                    unitOfWork.Comit();
                    return user;
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        [OperationContract]
        public void DeletedUser(Guid userGuid)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    generalService.DeleteUser(userGuid);
                    unitOfWork.Comit();
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }
        #endregion

        #region MenuCategory
        [OperationContract]
        public MenuCategory GetMenuCategoryByKey(Guid key)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    var result = generalService.GetMenuCategoryByKey(key);
                    return result;
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }


        [OperationContract]
        public List<MenuCategory> GetMenuCategory(Expression<Func<MenuCategory, bool>> where)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    var result = generalService.GetMenuCategory(where);
                    return result;
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        [OperationContract]
        public MenuCategory SaveMenuCategory(MenuCategory entity, bool isSkipVersion)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {

                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    generalService.SaveMenuCategory(entity, isSkipVersion);
                    unitOfWork.Comit();
                    return entity;
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }
        [OperationContract]
        public void DeleteMenuCategoryByKey(Guid key)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {

                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    generalService.DeleteMenuCategoryByKey(key);
                    unitOfWork.Comit();
                    return;
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        #endregion

        #region ProductCategory
        [OperationContract]
        public ProductCategory GetProductCategoryByKey(Guid key)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    var result = generalService.GetProductCategoryByKey(key);
                    return result;
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        [OperationContract]
        public ProductCategory GetProductCategoryFullPropertiesByKey(Guid key)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    var result = generalService.GetProductCategoryFullPropertiesByKey(key);
                    return result;
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }


        [OperationContract]
        public List<ProductCategory> GetProductCategory(Expression<Func<ProductCategory, bool>> where)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    var result = generalService.GetProductCategory(where);
                    return result;
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        [OperationContract]
        public ProductCategory SaveProductCategory(ProductCategory entity, bool isSkipVersion)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    generalService.SaveProductCategory(entity, isSkipVersion);
                    unitOfWork.Comit();
                    return entity;
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }
        [OperationContract]
        public void DeleteProductCategoryByKey(Guid key)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {

                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    generalService.DeleteProductCategoryByKey(key);
                    unitOfWork.Comit();
                    return;
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }
        #endregion

        #region Product
        [OperationContract]
        public Product GetProductFullPropertiesByKey(Guid key)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    var result = generalService.GetProductFullPropertiesByKey(key);
                    return result;
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }
        [OperationContract]
        public Product GetProductByKey(Guid key)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    var result = generalService.GetProductByKey(key);
                    return result;
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }
        [OperationContract]
        public List<Product> GetProductByCategory(string keyWord, Guid? categoryGuid = null, bool isSubCategory = true)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    var result = generalService.GetProductByCategory(keyWord, categoryGuid, isSubCategory);
                    return result;
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        [OperationContract]
        public List<Product> GetProduct(Expression<Func<Product, bool>> where)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    var result = generalService.GetProduct(where);
                    return result;
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        [OperationContract]
        public Product SaveProduct(Product entity, bool isSkipVersion)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {

                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    generalService.SaveProduct(entity, isSkipVersion);
                    unitOfWork.Comit();
                    return entity;
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }
        [OperationContract]
        public void DeleteProductByKey(Guid key)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {

                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    generalService.DeleteProductByKey(key);
                    unitOfWork.Comit();
                    return;
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }
        #endregion

        #region PropertiesDefinition
        [OperationContract]
        public List<PropertiesDefinition> GetAllPropertiesDef()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    var result = generalService.GetAllPropertiesDef();
                    return result;
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }
        #endregion

        #region RelationProperty
        [OperationContract]
        public void AddRelationOfProperties(Dictionary<Guid, string> propertyGuids, Guid guidCategory, string dicriminator)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    generalService.AddRelationOfProperties(propertyGuids, guidCategory, dicriminator);
                    unitOfWork.Comit();
                    return;
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }
        [OperationContract]
        public void DeleteRelationOfProperties(List<Guid> propertyGuids, Guid guidCategory, string dicriminator)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    generalService.DeleteRelationOfProperties(propertyGuids, guidCategory, dicriminator);
                    unitOfWork.Comit();
                    return;
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }
        #endregion

        #region Function for Manufacture
        [OperationContract]
        public Manufacture SaveManufacture(Manufacture obj)
        {
            try
            {

                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    generalService.SaveManufacture(obj);
                    unitOfWork.Comit();
                    return obj;


                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        [OperationContract]
        public List<Manufacture> GetManufacture(Expression<Func<Manufacture, bool>> where)
        {
            try
            {

                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    var result = generalService.GetManufacture(where);
                    return result;

                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }


        [OperationContract]
        public Manufacture GetManufactureByKey(Guid key)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    var result = generalService.GetManufactureByKey(key);
                    return result;


                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        [OperationContract]
        public void DeleteManufactureByKey(Guid key)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    generalService.DeleteManufactureByKey(key);
                    unitOfWork.Comit();

                    return;


                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        #endregion

        #region Function for Gallery
        [OperationContract]
        public Gallery SaveGallery(Gallery obj)
        {
            try
            {

                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    generalService.SaveGallery(obj);
                    unitOfWork.Comit();
                    return obj;


                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        [OperationContract]
        public List<Gallery> GetGallery(Expression<Func<Gallery, bool>> where)
        {
            try
            {

                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    var result = generalService.GetGallery(where);
                    return result;

                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }


        [OperationContract]
        public Gallery GetGalleryByKey(Guid key)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    var result = generalService.GetGalleryByKey(key);
                    return result;


                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        [OperationContract]
        public void DeleteGalleryByKey(Guid key)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    generalService.DeleteGalleryByKey(key);
                    unitOfWork.Comit();

                    return;


                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        #endregion 

        #region Function for Image
        [OperationContract]
        public Image SaveImage(Image obj)
        {
            try
            {

                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    generalService.SaveImage(obj);
                    unitOfWork.Comit();
                    return obj;


                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        [OperationContract]
        public List<Image> GetImage(Expression<Func<Image, bool>> where)
        {
            try
            {

                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    var result = generalService.GetImage(where);
                    return result;

                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }


        [OperationContract]
        public Image GetImageByKey(Guid key)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    var result = generalService.GetImageByKey(key);
                    return result;


                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        [OperationContract]
        public void DeleteImageByKey(Guid key)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    generalService.DeleteImageByKey(key);
                    unitOfWork.Comit();

                    return;


                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        #endregion 

        #region Function for Rating
        [OperationContract]
        public Rating SaveRating(Rating obj)
        {
            try
            {

                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    generalService.SaveRating(obj);
                    unitOfWork.Comit();
                    return obj;


                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        [OperationContract]
        public List<Rating> GetRating(Expression<Func<Rating, bool>> where)
        {
            try
            {

                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    var result = generalService.GetRating(where);
                    return result;

                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }


        [OperationContract]
        public Rating GetRatingByKey(Guid key)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    var result = generalService.GetRatingByKey(key);
                    return result;


                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        [OperationContract]
        public void DeleteRatingByKey(Guid key)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    generalService.DeleteRatingByKey(key);
                    unitOfWork.Comit();

                    return;


                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        #endregion 

        #region Function for Comment
        [OperationContract]
        public Comment SaveComment(Comment obj)
        {
            try
            {

                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    generalService.SaveComment(obj);
                    unitOfWork.Comit();
                    return obj;


                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        [OperationContract]
        public List<Comment> GetComment(Expression<Func<Comment, bool>> where)
        {
            try
            {

                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    var result = generalService.GetComment(where);
                    return result;

                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }


        [OperationContract]
        public Comment GetCommentByKey(Guid key)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    var result = generalService.GetCommentByKey(key);
                    return result;


                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        [OperationContract]
        public void DeleteCommentByKey(Guid key)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    generalService.DeleteCommentByKey(key);
                    unitOfWork.Comit();

                    return;


                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        #endregion 
        
        #region Function for menu
        [OperationContract]
        public CategoryCollection GetCategoryForIndexMenu(string codeLang)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();
                    var result = generalService.GetCategoryForIndexMenu(codeLang);
                    return result;
                   // return new CategoryCollection();
                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }
        #endregion

        #region Function for Language
        [OperationContract]
        public Language SaveLanguage(Language obj)
        {
            try
            {

                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    generalService.SaveLanguage(obj);
                    unitOfWork.Comit();
                    return obj;


                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        [OperationContract]
        public List<Language> GetLanguage(Expression<Func<Language, bool>> where)
        {
            try
            {

                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    var result = generalService.GetLanguage(where);
                    return result;

                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }


        [OperationContract]
        public Language GetLanguageByKey(Guid key)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    var result = generalService.GetLanguageByKey(key);
                    return result;


                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        [OperationContract]
        public void DeleteLanguageByKey(Guid key)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork<DBServerContext>())
                {
                    var generalService = unitOfWork.GetService<BusinessServerService>();

                    generalService.DeleteLanguageByKey(key);
                    unitOfWork.Comit();

                    return;


                }
            }
            catch (Exception ex)
            {
                //Todo log file
                throw ex;
            }
        }

        #endregion 

    }
}
