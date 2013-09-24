using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.BussinessLayer.Clients.Base;
using CMS.Entities.ClientObjects;
using CMS.Kernel;

namespace CMS.BussinessLayer.Clients
{
    public class BusinessClientService : BussinessServiceClientBase
    {
        #region Constructor methods
        public BusinessClientService(DbContextRepository ct)
            : base(ct)
        {
        }
        #endregion

        #region News region
        public void SaveNews(CONews newsToCreate)
        {
            //GenericRepository peopleRepository = new PersonRepository();
            db.SaveEntity(newsToCreate);
        }

        public IEnumerable<CONews> GetNews()
        {
            return db.Find<CONews>();
        }

        public CONews GetByID(long id)
        {
            return db.FindFirst<CONews>(f => f.EntityId == id);
        }

        public string DeleteNews(object id, bool isSoftDeleted = true)
        {
            try
            {
                var entityDelete = db.FindByKey<CONews>(id);
                db.Delete(entityDelete, isSoftDeleted);
                return "Success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string SaveMultiNews(string title1, string title2)
        {
            try
            {
                List<CONews> newsL = new List<CONews>();
                CONews new1 = new CONews();
                new1 = this.GetByID(11);
                //HelperLibrary.ConvertHelper.ConvertEntity(getValue, new1);
                new1.Content = "Content 1 Test trace";
                new1.Tags = "Tag1www cccccccc";
                new1.Title = title1;
                //new1.GuidId = Guid.NewGuid();
                newsL.Add(new1);
                CONews new2 = new CONews();
                new2.Content = "Content 2";
                new2.Tags = "Tag2";
                new2.Title = title2;
                newsL.Add(new2);
                db.SaveEntities<CONews>(newsL);
                return "Success";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<CONews> UpdateMultiNews()
        {
            return db.SaveEntities<CONews>(GetNews(), false);


        }
        #endregion


        #region Users Region
        public void SaveUser(COUsers user)
        {
            db.SaveEntity(user);
        }

        public void DeleteUser(Guid guiId)
        {
            db.DeleteByKey<COUsers>(guiId);
        }
        #endregion

        #region GroupCustomerSupplier Region
        public void SaveGroupCustomerSupplier(COGroupCustomerSupplier info)
        {
            db.SaveEntity(info);
        }

        public void DeleteGroupCustomerSupplier(Guid guiId)
        {
            db.DeleteByKey<COGroupCustomerSupplier>(guiId);
        }

        public void DeleteGroupCustomerSuppliers(IEnumerable<COGroupCustomerSupplier> entities)
        {
            db.DeleteEntities<COGroupCustomerSupplier>(entities);
        }

        public List<COGroupCustomerSupplier> GetAllGroupCustomerSupplier(string discriminator)
        {
            if (string.IsNullOrEmpty(discriminator))
                return db.Find<COGroupCustomerSupplier>().ToList();
            var result = db.Find<COGroupCustomerSupplier>(f => f.Discriminator == discriminator && f.IDBelong == null, StaticMethods.CreateInclude<COGroupCustomerSupplier>(i => i.Parent)).ToList();
            foreach (var item in result)
            {
                item.ListCOGroupCustomerSupplier = db.Find<COGroupCustomerSupplier>(x => x.IDBelong == item.GuidId).ToList();
            }
            return result;
        }

        public COGroupCustomerSupplier GetByIDGroupCustomerSupplier(string discriminator, int Id)
        {
            if (string.IsNullOrEmpty(discriminator))
                return db.FindFirst<COGroupCustomerSupplier>();
            return db.FindFirst<COGroupCustomerSupplier>(f => f.Discriminator == discriminator && f.EntityId == Id, StaticMethods.CreateInclude<COGroupCustomerSupplier>(i => i.Parent, o => o.ListCOGroupCustomerSupplier),false);

        }
        #endregion

    }

}
