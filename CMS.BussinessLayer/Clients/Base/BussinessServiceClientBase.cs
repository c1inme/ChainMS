using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Entities.ClientObjects;
using CMS.Kernel;
using CMS.Entities;

namespace CMS.BussinessLayer.Clients.Base
{
    public class BussinessServiceClientBase : BaseService
    {
        #region All event must be register here
        public BussinessServiceClientBase(DbContextRepository ct)
            : base(ct)
        {
            db.RegisterSaveChanges(SaveChanges);
            db.RegisterTraceChange(TraceChange);
            db.RegisterNotification<COGroupCustomerSupplier>(NotifyEnum.BeforeSave, BeforeSaveCOGroupCustomerSupplier);
        }
        #endregion

        protected void BeforeSaveCOGroupCustomerSupplier(object sender, EventNotifyEntity e)
        {
            COGroupCustomerSupplier group = e.Entity as COGroupCustomerSupplier;
            var result = db.Find<COGroupCustomerSupplier>(f => f.CodeGroup.Trim().ToLower() == group.CodeGroup.Trim().ToLower() && f.Discriminator == group.Discriminator);

            if (result != null && result.Count() > 0)
            {
                if (group.EntityId > 0 && result.Count() == 1 && result.FirstOrDefault().GuidId == group.GuidId) //Forcase update
                    return;

                e.IsCancel = true;
                throw new InvalidEntityException("General_CodeDupplicate");
            }
        }

        protected void SaveChanges(object sender, EntityChangeArgs e)
        {
            if (e.NameOfEntity == "TableLastModified" || e.NameOfEntity == "COTableLastModified")
                return;
            var record = db.FindFirst<COTableLastModified>(f => f.TableName == e.NameOfEntity);
            if (record == null)
                record = new COTableLastModified();
            record.TableName = e.NameOfEntity;
            record.ModifyDate = e.DateChange;
            db.SaveEntity<COTableLastModified>(record, false, true);
        }

        protected void TraceChange(object sender, TraceChangesArgs e)
        {
            COTraceChanges itemChange;
            List<COTraceChanges> TraceCollection = null;
            foreach (var it in e.ChangeCollection)
            {
                itemChange = new COTraceChanges();
                HelperLibrary.ConvertHelper.ConvertEntity(it, itemChange);
                if (TraceCollection == null)
                    TraceCollection = new List<COTraceChanges>();
                TraceCollection.Add(itemChange);
            }
            db.SaveEntities(TraceCollection, false);
        }
    }
}
