using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CMS.Kernel
{
    public abstract class BaseRepository : IRepository
    {

        public abstract E SaveEntity<E>(E entity, bool isTraceChange = false, bool isSkipVersion = false,bool isSystemChange = false) where E : class;

        public abstract IEnumerable<E> SaveEntities<E>(IEnumerable<E> entities, bool isTraceChange = false, bool isSkipVersion = false, bool isSystemChange = false) where E : class;

        public abstract E Insert<E>(E entity) where E : class;

        public abstract E Update<E>(E entity) where E : class;


        public abstract void Delete<E>(E entity, bool isSoftDeleted = true) where E : class;
        public abstract void DeleteEntities<E>(IEnumerable<E> entities, bool isSoftDeleted = true) where E : class;
        public abstract void DeleteByKey<E>(object entityKey, bool isSoftDeleted = true) where E : class;

        public int SaveChanges()
        {
            return SaveChanges(true);
        }

        public abstract int SaveChanges(bool validateEntities);

        public abstract IQueryable<E> Select<E>(Expression<Func<E, object>>[] includes, bool isDelete = false) where E : class;


        public abstract void Dispose();
    }
}
