using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Data.Objects;
using System.Data.Entity.Infrastructure;
using HelperLibrary.TraceChange;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Security.Principal;
using System.Web;
namespace CMS.Kernel
{
    public delegate void EntityNotificationHandler(object sender, EventNotifyEntity e);
    public delegate void SaveChangeHandler(object sender, EntityChangeArgs e);
    public delegate void TraceChangeHandler(object sender, TraceChangesArgs e);
    public class DbContextRepository : BaseRepository
    {

        #region CreateContext to trace change
        DbContext _contextCheck;
        DbContext ContextCheck
        {
            get
            {
                if (_contextCheck == null)
                    _contextCheck = (DbContext)Activator.CreateInstance(typeContext);
                return _contextCheck;
            }
        }
        #endregion

        #region Notify event
        private Dictionary<string, List<object>> registrations = new Dictionary<string, List<object>>();
        private Dictionary<string, List<object>> getDataHandlers = new Dictionary<string, List<object>>();

        #region Trace change event
        TraceChangeHandler traceChangeHandler;
        /// <summary>
        /// trace change for table log
        /// 
        /// </summary>
        /// <param name="handler"></param>
        public void RegisterTraceChange(TraceChangeHandler handler)
        {
            if (traceChangeHandler == null)
                traceChangeHandler = handler;
        }

        protected void RunTraceChange(object sender, TraceChangesArgs args)
        {
            if (traceChangeHandler != null)
                traceChangeHandler(sender, args);
        }
        #endregion

        #region Save change Event
        SaveChangeHandler handlerChange;
        /// <summary>
        /// Using for sync from client with server
        /// 
        /// </summary>
        /// <param name="handler"></param>
        public void RegisterSaveChanges(SaveChangeHandler handler)
        {
            if (handlerChange == null)
                handlerChange = handler;
        }

        protected void RunSaveChange(object sender, EntityChangeArgs args)
        {
            if (handlerChange != null)
                handlerChange(sender, args);
        }
        #endregion

        public void RegisterNotification<T>(NotifyEnum notificationType, EntityNotificationHandler handler) where T : class
        {
            if (handler == null)
                throw new ArgumentNullException("handler");
            string key = typeof(T).Name + notificationType.ToString();
            List<object> handlers = null;
            if (registrations.ContainsKey(key))
                handlers = registrations[key];
            else
            {
                handlers = new List<object>();
                registrations.Add(key, handlers);
            }
            handlers.Add(handler);
        }



        public void UnregisterNotification<T>(NotifyEnum notificationType, EntityNotificationHandler handler) where T : class
        {
            if (handler == null)
                throw new ArgumentNullException("handler");
            string key = typeof(T).Name + notificationType.ToString();
            if (registrations.ContainsKey(key))
            {
                List<object> handlers = registrations[key];
                handlers.Remove(handler);
            }
        }



        protected void RunNotification<T>(object sender, NotifyEnum notificationType, EventNotifyEntity args) where T : class
        {
            if (args == null)
                throw new ArgumentNullException("args");
            //var entityDef = DataDefinition.GetEntityDefinition(typeof(T).Name);

            //IList<EntityDefinition> entitiesToNotify = new List<EntityDefinition>();
            //while (entityDef != null)
            //{
            //    entitiesToNotify.Insert(0, entityDef);
            //    entityDef = entityDef.ParentEntity;
            //}

            //foreach (var entity in entitiesToNotify)
            //{
            string key = typeof(T).Name + notificationType.ToString();
            if (registrations.ContainsKey(key))
            {
                List<object> handlers = registrations[key];

                foreach (EntityNotificationHandler entityHandler in handlers)
                    entityHandler(sender, args);
            }
            //}
        }
        #endregion

        #region Fields

        private DbContext _context;

        #endregion

        #region Properties
        public DbContext Context
        {
            get
            {
                if (_context == null)
                    throw new Exception("context is null ");
                return _context;
            }
        }
        Type typeContext;
        public DbContextRepository(object ctext)
        {
            _context = (DbContext)ctext;
            typeContext = ctext.GetType();
        }
        #endregion

        #region Methods

        public override IEnumerable<E> SaveEntities<E>(IEnumerable<E> entities, bool isTraceChange = false, bool isSkipVersion = false, bool isSystemChange = false)
        {
            var args = new EventNotifyEntity();
            args.Entities = entities;
            RunNotification<E>(this, NotifyEnum.BeforeSaveMulti, args);
            if (args.IsCancel)
                return entities;
            foreach (E it in entities)
            {
                IEntity enti = it as IEntity;
                if (!isSystemChange)
                    enti.ModifyBy = StaticMethods.GuidCurrentUser();
                if (enti.GuidId == Guid.Empty || enti.GuidId == null)
                    enti.GuidId = Guid.NewGuid();
                if (enti == null)
                    throw new Exception("Entity must be inherit from IEntity");
                if (enti.EntityId > 0)
                {

                    var oldEntity = this.ContextCheck.Set<E>().Find(enti.GuidId);
                    if (oldEntity == null)
                        throw new Exception("Entity not exist, it maybe had deleted");
                    IEntity oldEnti = oldEntity as IEntity;
                    if (enti.VersionNumber < oldEnti.VersionNumber)
                        throw new Exception("Current version is out up date");
                    if (isTraceChange)
                    {
                        InfoChangeCollection changes = HelperLibrary.TraceChanges.GetChange(oldEntity, it, oldEnti.GuidId);
                        if (changes == null || changes.Count == 0)
                            continue;
                        TraceChangesArgs argsTraceChange = new TraceChangesArgs(changes);
                        RunTraceChange(this, argsTraceChange);
                    }
                    enti.ModifyDate = DateTime.Now;

                    if (!isSkipVersion)
                        enti.VersionNumber++;

                    Context.Set<E>().Attach(it);
                    Context.Entry<E>(it).State = EntityState.Modified;
                }
                else
                {
                    enti.CreateDate = DateTime.Now;
                    enti.ModifyDate = DateTime.Now;
                    enti.VersionNumber = 1;
                    if (!isSystemChange)
                        enti.CreateBy = StaticMethods.GuidCurrentUser();
                    Context.Set<E>().Attach(it);
                    Context.Entry<E>(it).State = EntityState.Added;
                    //Context.Set<E>().Add(entity);
                    this.SaveChanges();
                }
            }
            this.SaveChanges();
            RunNotification<E>(this, NotifyEnum.AfterSaveMulti, args);
            EntityChangeArgs argsChange = new EntityChangeArgs(typeof(E).Name, DateTime.Now, entities);
            RunSaveChange(this, argsChange);
            return entities;
        }

        public override E SaveEntity<E>(E entity, bool isTraceChange = false, bool isSkipVersion = false, bool isSystemChange = false)
        {

            IEntity enti = entity as IEntity;
            if (enti.GuidId == Guid.Empty || enti.GuidId == null)
                enti.GuidId = Guid.NewGuid();
            if (!isSystemChange)
                enti.ModifyBy = StaticMethods.GuidCurrentUser();
            E result;
            if (enti == null)
                throw new Exception("Entity must be inherit from IEntity");
            var args = new EventNotifyEntity(entity);
            RunNotification<E>(this, NotifyEnum.BeforeSave, args);
            if (args.IsCancel)
                return entity;
            E oldEntity = null;
            if (!isSystemChange)
                oldEntity = this.ContextCheck.Set<E>().Find(enti.GuidId);
            else
                oldEntity = entity;
            IEntity oldEnti = oldEntity as IEntity;
            if (oldEnti != null && oldEnti.EntityId > 0)
            {
                if (oldEnti.Deleted == true)
                    throw new Exception("Entity not exist, it maybe had deleted");
                if (enti.VersionNumber < oldEnti.VersionNumber)
                    throw new Exception("Current version is out up date");
                if (isTraceChange)
                {
                    InfoChangeCollection changes = HelperLibrary.TraceChanges.GetChange(oldEntity, entity, oldEnti.GuidId);
                    if (changes == null || changes.Count > 0)
                        return entity;//For case nothing change
                    TraceChangesArgs argsTraceChange = new TraceChangesArgs(changes);
                    RunTraceChange(this, argsTraceChange);
                }
                enti.CreateBy = oldEnti.CreateBy;
                enti.CreateDate = oldEnti.CreateDate;
                enti.VersionNumber = oldEnti.VersionNumber;
                enti.EntityId = oldEnti.EntityId;
                enti.ModifyDate = DateTime.Now;
                //enti.ModifyBy = 
                if (!isSkipVersion)
                    enti.VersionNumber++;
                result = Update(entity);
            }
            else
            {
                enti.CreateDate = DateTime.Now;
                enti.ModifyDate = DateTime.Now;
                enti.VersionNumber = 1;
                if (!isSystemChange)
                    enti.CreateBy = StaticMethods.GuidCurrentUser();
                //enti.ModifyBy = 
                result = Insert(entity);
            }
            RunNotification<E>(this, NotifyEnum.AfterSave, args);
            EntityChangeArgs argsChange = new EntityChangeArgs(typeof(E).Name, DateTime.Now, entity);
            RunSaveChange(this, argsChange);
            return result;
        }

        public override E Insert<E>(E entity)
        {

            Context.Set<E>().Attach(entity);
            Context.Entry<E>(entity).State = EntityState.Added;
            //Context.Set<E>().Add(entity);
            this.SaveChanges();
            return entity;
        }

        public override E Update<E>(E entity)
        {
            if (Context.Entry<E>(entity).State == EntityState.Detached)
            {
                IEntity it = entity as IEntity;
                var attachedEntity = Context.Set<E>().Find(it.GuidId);
                if (attachedEntity != null)
                {
                    var attachedEntry = Context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    Context.Entry<E>(entity).State = EntityState.Modified;
                }
                //UpdatePropertiesSystem(entity);
                //Context.Set<E>().Attach(entity);
                //Context.Entry<E>(entity).State = EntityState.Modified;
            }
            this.SaveChanges();
            return entity; ;
        }

        public void UpdatePropertiesSystem<T>(T entity) where T : class
        {
            Type _type = entity.GetType();
            var properties = _type.GetProperties();
            var entry = _context.Entry(entity);
            foreach (var prop in properties)
            {
                switch (prop.Name)
                {
                    case "CreateBy":
                    //  case "ModifyBy":
                    // case "ModifyDate":
                    case "CreateDate":
                    case "EntityId":
                        //case "VersionNumber":
                        //case "Deleted":
                        entry.Property(prop.Name).IsModified = false;
                        break;
                    default:
                        break;

                }
            }
        }


        public override void Delete<E>(E entity, bool isSoftDeleted = true)
        {
            var args = new EventNotifyEntity(entity);
            RunNotification<E>(this, NotifyEnum.BeforeDeleted, args);
            if (args.IsCancel)
                return;
            if (!isSoftDeleted)
            {
                Context.Set<E>().Attach(entity);
                Context.Entry<E>(entity).State = EntityState.Deleted;
                Context.SaveChanges();
            }
            else
            {
                //Soft deleted
                IEntity enti = entity as IEntity;
                enti.Deleted = true;
                enti.ModifyBy = StaticMethods.GuidCurrentUser();
                Update(entity);
                ((IObjectContextAdapter)_context).ObjectContext.Detach(entity);
            }
            RunNotification<E>(this, NotifyEnum.AfterDeleted, args);
        }

        public override void DeleteEntities<E>(IEnumerable<E> entities, bool isSoftDeleted = true)
        {
            var args = new EventNotifyEntity();
            args.Entities = entities;
            RunNotification<E>(this, NotifyEnum.BeforeDeletedMulti, args);
            if (args.IsCancel)
                return;
            foreach (E it in entities)
            {
                IEntity enti = it as IEntity;
                if (enti == null)
                    throw new Exception("Entity must be inherit from IEntity");
                if (isSoftDeleted)
                {
                    enti.ModifyDate = DateTime.Now;
                    enti.ModifyDate = DateTime.Now;
                    enti.VersionNumber++;
                    enti.Deleted = true;
                    enti.ModifyBy = StaticMethods.GuidCurrentUser();
                    Context.Set<E>().Attach(it);
                    Context.Entry<E>(it).State = EntityState.Modified;
                }
                else
                {
                    Context.Set<E>().Attach(it);
                    Context.Entry<E>(it).State = EntityState.Deleted;
                    ((IObjectContextAdapter)_context).ObjectContext.Detach(it);
                }
            }
            this.SaveChanges();
            RunNotification<E>(this, NotifyEnum.AfterDeletedMulti, args);
            EntityChangeArgs argsChange = new EntityChangeArgs(typeof(E).Name, DateTime.Now, entities);
            RunSaveChange(this, argsChange);
        }

        public void DeleteEntitiesByKey<E>(IEnumerable<object> entityKeys, bool isSoftDeleted = true) where E : class
        {
            List<E> entities = new List<E>();
            foreach (object obj in entityKeys)
            {
                var it = FindByKey<E>(obj, true);
                entities.Add(it);
            }
            if (entities.Count == 0)
                throw new Exception("Can not found entityKeys  in table " + typeof(E).Name);
            DeleteEntities(entities, isSoftDeleted);
        }

        public override void DeleteByKey<E>(object entityKey, bool isSoftDeleted = true)
        {
            var entity = FindByKey<E>(entityKey);
            if (entity == null)
                throw new Exception("Can not found entityKey = " + entityKey.ToString() + " in table =" + typeof(E).Name);
            Delete(entity, isSoftDeleted);
        }

        public override int SaveChanges(bool validateEntities)
        {
            if (!Context.Database.Connection.IsDatabaseOnline())
                throw new Exception("The database is currently not online.");

            Context.Configuration.ValidateOnSaveEnabled = validateEntities;
            var rowEffect = Context.SaveChanges();
            if (rowEffect < 0)
                throw new Exception("Save error");
            return rowEffect;
        }

        public override IQueryable<E> Select<E>(Expression<Func<E, object>>[] includes = null, bool isDelete = false)
        {
            if (!Context.Database.Connection.IsDatabaseOnline())
                throw new Exception("The database is currently not online.");
            IQueryable<E> qr = null;
            qr = Context.Set<E>().AsQueryable();
            object[] paras = new object[] { false };

            if (includes != null)
                foreach (var inc in includes)
                {
                    // Retrieve member path:  
                    //List<PropertyInfo> members = new List<PropertyInfo>();
                    //CMS.Kernel.Utility.DataObjectExtension.EntityFrameworkHelper.CollectRelationalMembers(inc, members);

                    // Build string path:  
                    //StringBuilder sb = new StringBuilder();
                    //string separator = "";
                    //foreach (MemberInfo member in members)
                    //{
                    //    sb.Append(separator);
                    //    sb.Append(member.Name);

                    //    separator = ".";
                    //}
                    //if (CMS.Kernel.Utility.DataObjectExtension.EntityFrameworkHelper.IsCollection(members.LastOrDefault()))
                    //    qr = qr.Include(sb.ToString()).Where(string.Format("{0}.Where(it.Deleted.HasValue.Equals(@0) Or it.Deleted.Value.Equals(@0))", sb.ToString()), paras);
                    //else
                    //    qr = qr.Include(sb.ToString()).Where(string.Format("it.{0}.Deleted.HasValue.Equals(@0) Or it.{0}.Deleted.Value.Equals(@0)", sb.ToString()), paras);

                    qr = qr.Include(inc);
                }

            if (!isDelete)
                return qr.Where("it.Deleted.HasValue.Equals(@0) Or it.Deleted.Value.Equals(@0)", paras);


            return qr;
        }


        //For case get isDelete = false
        public IQueryable<E> GetIEntity<E>() where E : class
        {
            object[] paras = new object[] { false };
            return Context.Set<E>().Where("it.Deleted.HasValue.Equals(@0) Or it.Deleted.Value.Equals(@0)", paras);
        }



        public override void Dispose()
        {
            if (_context != null)
                _context.Dispose();
            if (_contextCheck != null)
                _contextCheck.Dispose();
        }

        #endregion

        #region Methods

        public BaseRepository Repository
        {
            get
            {
                if (this == null)
                    throw new Exception("Error : Context cannot null");
                return this as BaseRepository;
            }
        }

        internal virtual void ExecuteQuery(Action<IRepository> query)
        {
            if (query == null)
                throw new ArgumentException("The query argument can not be null.");
            var queryRepository = Repository;
            query(queryRepository);
        }



        public virtual IEnumerable<E> Find<E>(Expression<Func<E, object>>[] includes = null, bool isDelete = false) where E : class
        {
            IEnumerable<E> results = null;

            ExecuteQuery(query =>
            {
                results = query.Select<E>(includes, isDelete);
#if DEBUG
                string sql = results.ToString();
                WriteSQLDebug(sql);
#endif

            });

            return results;
        }

        private void WriteSQLDebug(string sql)
        {
            Debug.WriteLine(sql);
        }
        public virtual IEnumerable<E> Find<E>(int rowNumber, int pageSize, Expression<Func<E, object>>[] includes = null, bool isDelete = false) where E : class
        {
            // There is a bug in the EF stack that does not allow you to call the Skip()
            // function without calling the OrderBy() function.
            string defaultSort = StaticMethods.CreateSortExpression<E>(string.Empty, string.Empty);

            IEnumerable<E> results = null;

            ExecuteQuery(query =>
            {
                results = query.Select<E>(includes, isDelete).AsQueryable<E>().OrderBy(defaultSort).Skip(rowNumber).Take(pageSize);
#if DEBUG
                string sql = results.ToString();
                WriteSQLDebug(sql);
#endif

            });

            return results;
        }

        public virtual IEnumerable<E> Find<E>(Expression<Func<E, object>>[] sortBy, int rowNumber, int pageSize, Expression<Func<E, object>>[] includes = null, bool isDelete = false) where E : class
        {
            if (sortBy == null)
                return Find<E>(rowNumber, pageSize);

            IEnumerable<E> results = null;

            ExecuteQuery(query =>
            {
                var qr = query.Select<E>(includes, isDelete);
                foreach (var order in sortBy)
                    qr = qr.OrderBy(order);
                results = qr.Skip(rowNumber).Take(pageSize);
#if DEBUG
                string sql = results.ToString();
                WriteSQLDebug(sql);
#endif

            });

            return results;
        }

        public virtual IEnumerable<E> Find<E>(Expression<Func<E, bool>> where, Expression<Func<E, object>>[] includes = null, bool isDelete = false) where E : class
        {
            if (where == null)
                return Find<E>(includes);

            IEnumerable<E> results = null;

            ExecuteQuery(query =>
            {
                results = query.Select<E>(includes, isDelete).Where<E>(where);
#if DEBUG
                string sql = results.ToString();
                WriteSQLDebug(sql);
#endif

            });

            return results;
        }

        public virtual IEnumerable<E> Find<E>(Expression<Func<E, bool>> where, int rowNumber, int pageSize, Expression<Func<E, object>>[] includes = null, bool isDelete = false) where E : class
        {
            if (where == null)
                return Find<E>(rowNumber, pageSize, includes);

            // There is a bug in the EF stack that does not allow you to call the Skip()
            // function without calling the OrderBy() function.
            string defaultSort = StaticMethods.CreateSortExpression<E>(string.Empty, string.Empty);

            IEnumerable<E> results = null;

            ExecuteQuery(query =>
            {
                results = query.Select<E>(includes, isDelete).Where<E>(where).AsQueryable<E>().OrderBy(defaultSort).Skip(rowNumber).Take(pageSize);
#if DEBUG
                string sql = results.ToString();
                WriteSQLDebug(sql);
#endif

            });

            return results;
        }

        public virtual IEnumerable<E> Find<E>(Expression<Func<E, bool>> where, Expression<Func<E, object>>[] sortBy, int rowNumber, int pageSize, Expression<Func<E, object>>[] includes = null, bool isDelete = false) where E : class
        {
            if (where == null)
                return Find<E>(sortBy, rowNumber, pageSize);

            if (sortBy != null)
                return Find(where, rowNumber, pageSize);

            IEnumerable<E> results = null;

            ExecuteQuery(query =>
            {
                var qr = query.Select<E>(includes, isDelete).Where<E>(where);
                if (sortBy != null)
                    foreach (var order in sortBy)
                        qr = qr.OrderBy(order);
                results = qr.Skip(rowNumber).Take(pageSize);

#if DEBUG
                string sql = results.ToString();
                WriteSQLDebug(sql);
#endif

            });

            return results;
        }

        public virtual E FindByKey<E>(object key, bool isDelete = false) where E : class
        {
            if (key == null)
                throw new ArgumentNullException("key", "The key argument can not be null.");

            if (!Context.Database.Connection.IsDatabaseOnline())
                throw new Exception("The database is currently not online.");

            var EResult = Context.Set<E>().Find(key);
            if (isDelete)
                return EResult;

            //Check Item is deleted.
            var result = EResult as IEntity;
            if (result != null && result.Deleted.HasValue && result.Deleted.Value)
                return null;
            return EResult;
        }

        public E FindFirst<E>(Expression<Func<E, object>>[] includes = null, bool isDelete = false) where E : class
        {
            E result = null;
            ExecuteQuery(query =>
            {
                var qr = query.Select<E>(includes, isDelete);
#if DEBUG
                string sql = qr.ToString();
                WriteSQLDebug(sql);
#endif
                result = qr.FirstOrDefault();
            });
            return result;
        }

        public E FindFirst<E>(Expression<Func<E, bool>> where, Expression<Func<E, object>>[] includes = null, bool isDelete = false) where E : class
        {
            E result = null;
            ExecuteQuery(query =>
            {
                var qr = query.Select<E>(includes, isDelete).Where(where).AsQueryable();

#if DEBUG
                string sql = qr.ToString();
                WriteSQLDebug(sql);
#endif
                result = qr.FirstOrDefault();
            });
            return result;
        }

        public E FindFirst<E>(Expression<Func<E, bool>> where, Expression<Func<E, object>>[] sortBy, Expression<Func<E, object>>[] includes = null, bool isDelete = false) where E : class
        {
            E result = null;
            ExecuteQuery(query =>
            {
                var qr = query.Select<E>(includes, isDelete).Where(where).AsQueryable();

                if (sortBy != null)
                    foreach (var order in sortBy)
                        qr = qr.OrderBy(order);
#if DEBUG
                string sql = qr.ToString();
                WriteSQLDebug(sql);
#endif
                result = qr.FirstOrDefault();
            });
            return result;
        }

        public virtual int RowCount<E>(bool isDelete = false) where E : class
        {
            int rowCount = -1;

            ExecuteQuery(query =>
            {
                rowCount = query.Select<E>(null, isDelete).Count();
            });

            return rowCount;
        }

        public virtual int RowCount<E>(Expression<Func<E, bool>> where, bool isDelete = false) where E : class
        {
            if (where == null)
                return RowCount<E>();

            int rowCount = -1;

            ExecuteQuery(query =>
            {
                rowCount = query.Select<E>(null, isDelete).Where<E>(where).Count();

            });

            return rowCount;
        }


        #endregion
    }
}
