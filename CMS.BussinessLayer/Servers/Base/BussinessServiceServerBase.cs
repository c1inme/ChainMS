using CMS.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Entities.ServerObjects;

namespace CMS.BussinessLayer.Servers.Base
{
    public class BussinessServiceServerBase : BaseService
    {
        #region All event must be register here
        public BussinessServiceServerBase(DbContextRepository ct)
            : base(ct)
        {
            // db.RegisterNotification<News>(NotifyEnum.BeforeSave, BeforeSaveNews);
            db.RegisterSaveChanges(SaveChanges);
            db.RegisterTraceChange(TraceChange);
        }
        #endregion
        //protected void BeforeSaveNews(object sender, EventNotifyEntity e)
        //{
        //    News newEntity = e.Entity as News;
        //    newEntity.Title = "Modify before save";
        //}

        protected void SaveChanges(object sender, EntityChangeArgs e)
        {
            if (e.NameOfEntity == "TableLastModified")
                return;
            var record = db.FindFirst<TableLastModified>(f => f.TableName == e.NameOfEntity);
            if (record == null)
                record = new TableLastModified();
            record.TableName = e.NameOfEntity;
            record.ModifyDate = e.DateChange;
            db.SaveEntity<TableLastModified>(record, false, false, true);
        }

        protected void TraceChange(object sender, TraceChangesArgs e)
        {
            TraceChanges itemChange;
            List<TraceChanges> TraceCollection = null;
            foreach (var it in e.ChangeCollection)
            {
                itemChange = new TraceChanges();
                HelperLibrary.ConvertHelper.ConvertEntity(it, itemChange);
                if (TraceCollection == null)
                    TraceCollection = new List<TraceChanges>();
                TraceCollection.Add(itemChange);
            }
            db.SaveEntities(TraceCollection, false, false, true);
        }
    }
}
